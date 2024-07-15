using BookLibraryApi.Interfaces.Repositories;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BookLibraryApi.Data.Repositories;

public class BookRepository:IBookRepository
{
    private readonly LibraryDbContext _dbContext;
    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public IQueryable<Book> GetBooksQueryBySearch(string searchParam = "")
    {
        try
        {
            return _dbContext.Books
                .Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving books with searchParam {searchParam}: {Message}", searchParam, ex.Message);
            throw;
        }
    }

    public async Task<Book> GetBookByIdAsync(int id,CancellationToken cancellationToken)
    {
        try
        {
            var result= await _dbContext.Books.FindAsync(id,cancellationToken);//TODO: check 
            if (result == null)
            {
                Log.Error( "Book is not found");
                throw new KeyNotFoundException("book not found");
            }

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving book with id {id}: {Message}", id, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(int bookId,CancellationToken cancellationToken)
    {
        try
        {
            return await _dbContext.Comments
                .Where(c => c.BookId == bookId)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving comments with book id {bookId}: {Message}", bookId, ex.Message);
            throw;
        }
    }

    public async Task<int> AddCommentAsync(Comment comment,CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Comments.Add(comment);
            return await _dbContext.SaveChangesAsync(cancellationToken);  
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error adding comment:  {comment}, {Message}", comment, ex.Message);
            throw;
        }  
    }
}