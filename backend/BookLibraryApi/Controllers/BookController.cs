using BookLibraryApi.Data;
using BookLibraryApi.Interfaces.Services;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi;
[Route("[controller]")]
public class BookController:ControllerBase
{
    private readonly LibraryDbContext _dbContext;
    private readonly IBookService _bookService;

    public BookController(LibraryDbContext dbContext, IBookService bookService)
    {
        _dbContext = dbContext;
        _bookService = bookService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int page = 1, int pageSize = 10, string searchParam = "" )
    {
        var response = await _bookService.GetBooksAsync(page, pageSize, searchParam);
        
        return Ok(response);
    }
    [HttpPost("{bookId}/comment")]
    public async Task<ActionResult<Comment>> AddComment(int bookId, [FromBody] string comment)
    {
        if (comment == null || string.IsNullOrEmpty(comment))
        {
            return BadRequest("Comment text is required.");
        }

        var res = await _bookService.AddCommentAsync(bookId, comment);
        return Ok(res);
    }
    [HttpGet("{bookId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int bookId)
    {
        var comments = await _bookService.GetCommentsAsync(bookId);
        return Ok(comments);
    }
}