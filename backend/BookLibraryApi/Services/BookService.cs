using BookLibraryApi.Interfaces.Services;
using BookLibraryApi.Interfaces.Repositories;
using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BookLibraryApi.Services;

public class BookService:IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
    }
    public async Task<GetBooksResponse> GetBooksAsync(int page, int pageSize, string search,CancellationToken cancellationToken)
    {
        var booksQueryable = _bookRepository.GetBooksQueryBySearch(search);
        var booksCount = await booksQueryable.CountAsync(cancellationToken);

        var pagesCount = (int)Math.Ceiling(booksCount / (double)pageSize);
        var books = await booksQueryable
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
  
        var response = new GetBooksResponse()
        {
            BooksCount = booksCount,
            PagesCount = pagesCount,
            CurrentPage = page,
            Books = books
        };
        return response;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(int bookId,CancellationToken cancellationToken)
    {
        return await _bookRepository.GetCommentsAsync(bookId,cancellationToken);
    }

    public async Task<int> AddCommentAsync(int bookId,string comment,CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(bookId,cancellationToken);
        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }
        var newComment = new Comment()
        {
            BookId = bookId,
            CreatedAt = DateTime.UtcNow,
            Content = comment,
            Book = book,
        };
        return await _bookRepository.AddCommentAsync(newComment,cancellationToken);
    }
}