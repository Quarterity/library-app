using BookLibraryApi.Interfaces.Services;
using BookLibraryApi.Interfaces.Repositories;
using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;

namespace BookLibraryApi.Services;

public class BookService:IBookService
{
    private IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<GetBooksResponse> GetBooksAsync(int page, int pageSize, string search)
    {
        var booksCount = _bookRepository.GetBooksCountAsync(search).Result;
        var pagesCount = (int)Math.Ceiling(booksCount / (double)pageSize);
        var books = _bookRepository.GetBooksAsync(page, pageSize, search);

        var response = new GetBooksResponse()
        {
            BooksCount = booksCount,
            PagesCount = pagesCount,
            CurrentPage = page,
            Books = books.Result
        };
        return response;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(int bookId)
    {
        return await _bookRepository.GetCommentsAsync(bookId);
    }

    public async Task<int?> AddCommentAsync(int bookId,string comment)
    {
        var book = _bookRepository.GetBookByIdAsync(bookId).Result;
        if (book == null)
        {
            return null;
        }
        var newComment = new Comment()
        {
            BookId = bookId,
            CreatedAt = DateTime.Now,
            Content = comment,
            Book = book,
        };
        return await _bookRepository.AddComment(newComment);
    }
}