using BookLibraryApi.Data;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi;
[Route("[controller]")]
public class BookController:ControllerBase
{
    private readonly LibraryDbContext _dbContext;

    public BookController(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int page = 1, int pageSize = 10, string searchParam = "" )
    {
        var booksCount = await _dbContext.Books.Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam)).CountAsync();
        var pagesCount = (int)Math.Ceiling(booksCount / (double)pageSize);

        var books = await _dbContext.Books
            .Where(s => string.IsNullOrEmpty(searchParam) || s.Title.Contains(searchParam))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var response = new
        {
            BooksCount = booksCount,
            PagesCount = pagesCount,
            CurrentPage = page,
            Books = books
        };
        
        return Ok(response);
    }
}