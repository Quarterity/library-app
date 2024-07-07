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
    [HttpPost("{bookId}/comment")]
    public async Task<ActionResult<Comment>> AddComment(int bookId, [FromBody] string comment)
    {
        if (comment == null || string.IsNullOrEmpty(comment))
        {
            return BadRequest("Comment text is required.");
        }

        var book = await _dbContext.Books.FindAsync(bookId);
        if (book == null)
        {
            return NotFound("Book not found.");
        }

        Comment newComment=new Comment();
        newComment.BookId = bookId;
        newComment.Content=comment;
        _dbContext.Comments.Add(newComment);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpGet("comments/{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _dbContext.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        return Ok(comment);
    }
    [HttpGet("{bookId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int bookId)
    {
        var comments = await _dbContext.Comments
            .Where(c => c.BookId == bookId)
            .ToListAsync();

        return Ok(comments);
    }

}