using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;
using Newtonsoft.Json;
using Serilog;

namespace BookLibraryApi.Data;

public class DbInitializer
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceProvider _serviceProvider;
    public DbInitializer(IHttpClientFactory httpClientFactory, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _httpClientFactory = httpClientFactory;
    }
    public async Task InitializeAsync()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            if (context.Books.Any())
            {
                Log.Information("Database already contains books, skipping initialization.");
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://freetestapi.com/api/v1/books");
            if (!response.IsSuccessStatusCode)
            {
                Log.Error("Failed to fetch books: {StatusCode}", response.StatusCode);
                throw new HttpRequestException($"Failed to fetch books: {response.StatusCode}");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<TestApiBook>>(responseBody);

            if (books == null || !books.Any())
            {
                Log.Warning("No books were retrieved from the API.");
                return;
            }
            var mappedBooks = books.Select(book => new Book
            {
                Title = book.Title??"Unknown Title",
                Author = book.Author?? "Unknown Author",
                PublicationYear = book.Publication_year??"Unknown",
                Description = book.Description??string.Empty,
                Picture = book.Cover_image??string.Empty
            }).ToList();
            if (!mappedBooks.Any())
            {
                Log.Warning("No valid books to insert into the database.");
                return;
            }
            context.Books.AddRange(mappedBooks);
            await context.SaveChangesAsync();
            Log.Information("Database has been initialized with {Count} books.", mappedBooks.Count);
        }
        catch (Exception ex)
        {
            Log.Error($"Error in initializer:{ex.Message}");
            throw;
        }
    }
}