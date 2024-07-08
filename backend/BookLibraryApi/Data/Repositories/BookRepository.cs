using BookLibraryApi.Interfaces.Repositories;
using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Data.Repositories;

public class BookRepository:IBookRepository
{
    private LibraryDbContext _dbContext;
    public BookRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Book>> GetBooksAsync(int page = 1, int pageSize = 10, string searchParam = "")
    {
        var books = await _dbContext.Books
            .Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return books;
    }

    public async Task<int> GetBooksCountAsync(string searchParam)
    {
         return await _dbContext.Books.Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam)).CountAsync();
    }
    
    
    public async Task<Book> GetBookByIdAsync(int id)
    {
        var result= await _dbContext.Books.FindAsync(id);
        if (result == null)
        {
            return null;
        }

        return result;
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(int bookId)
    {
        return await _dbContext.Comments
            .Where(c => c.BookId == bookId)
            .ToListAsync();
        
    }

    public async Task<int?> AddComment(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        return await _dbContext.SaveChangesAsync();    
    }
}