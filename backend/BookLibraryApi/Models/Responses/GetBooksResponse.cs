namespace BookLibraryApi.Models.Responses;

public class GetBooksResponse
{
    public IEnumerable<Book> Books { get; set; }
    public int BooksCount { get; set; }
    public int PagesCount { get; set; }
    public int CurrentPage { get; set; }
}