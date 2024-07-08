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
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Book>> GetBooksAsync(int page = 1, int pageSize = 10, string searchParam = "")
    {
        try
        {
            var books = await _dbContext.Books
                .Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
            return books;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving books with searchParam {searchParam}: {Message}", searchParam, ex.Message);
            throw;
        }
    }

    public async Task<int> GetBooksCountAsync(string searchParam)
    {
        try
        {
            return await _dbContext.Books.Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam)).CountAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving booksCount with searchParam {searchParam}: {Message}", searchParam, ex.Message);
            throw;
        }
    }
    
    
    public async Task<Book> GetBookByIdAsync(int id)
    {
        try
        {
            var result= await _dbContext.Books.FindAsync(id);
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

    public async Task<IEnumerable<Comment>> GetCommentsAsync(int bookId)
    {
        try
        {
            return await _dbContext.Comments
                .Where(c => c.BookId == bookId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error retrieving comments with book id {bookId}: {Message}", bookId, ex.Message);
            throw;
        }
    }

    public async Task<int?> AddComment(Comment comment)
    {
        try
        {
            _dbContext.Comments.Add(comment);
            return await _dbContext.SaveChangesAsync();  
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error adding comment with  {comment}: {Message}", comment, ex.Message);
            throw;
        }  
    }
}