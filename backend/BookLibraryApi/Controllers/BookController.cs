using BookLibraryApi.Interfaces.Services;
using BookLibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryApi.Controllers;
[Route("[controller]")]
[ApiController]
public class BookController:ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks(int page = 1, int pageSize = 10, string searchParam = "", CancellationToken cancellationToken =default)
    {
        var response = await _bookService.GetBooksAsync(page, pageSize, searchParam,cancellationToken);
        return Ok(response);
    }
    [HttpPost("{bookId}/comment")]
    public async Task<ActionResult<int>> AddComment(int bookId, [FromBody] string comment,CancellationToken cancellationToken=default)
    {
        if (string.IsNullOrEmpty(comment))
        {
            return BadRequest("Comment text is required.");
        }

        var result = await _bookService.AddCommentAsync(bookId, comment,cancellationToken);
        if (result != 1)
        {
            return NotFound($"Book with Id {bookId} is not found");
        }
        return Ok(result);
    }
    [HttpGet("{bookId}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments(int bookId,CancellationToken cancellationToken=default)
    {
        var comments = await _bookService.GetCommentsAsync(bookId,cancellationToken);
        return Ok(comments);
    }
}