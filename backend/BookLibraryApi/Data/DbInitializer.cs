using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;
using Newtonsoft.Json;

namespace BookLibraryApi.Data;

public class DbInitializer
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly LibraryDbContext _context;
    public DbInitializer(LibraryDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
    }
    public async Task Initialize()
    {
        if (_context.Books.Any())
        {
            return;
        }

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync("https://freetestapi.com/api/v1/books");
        var responseBody = await response.Content.ReadAsStringAsync();
        var books = JsonConvert.DeserializeObject<List<TestApiBook>>(responseBody);

        List<Book> mappedBooks=new List<Book>();
        if (books != null && books.Any())
        {
            foreach (var book in books)
            {
                mappedBooks.Add(new Book()
                {
                    Title = book.Title,
                    Author = book.Author,
                    PublicationYear = book.Publication_year,
                    Description = book.Description,
                    Picture = book.Cover_image
                });
            }
        }
        
        _context.Books.AddRange(mappedBooks);
        await _context.SaveChangesAsync();
    }
}