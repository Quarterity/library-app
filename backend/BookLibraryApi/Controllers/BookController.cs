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
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _dbContext.Books.ToListAsync();
    }
}