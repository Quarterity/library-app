using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace BookLibraryApi.Interfaces.Repositories;

public interface IBookRepository
{
    IQueryable<Book> GetBooksQueryBySearch(string search);

    Task<Book> GetBookByIdAsync(int id,CancellationToken cancellationToken);
    Task<IEnumerable<Comment>> GetCommentsAsync(int bookId,CancellationToken cancellationToken);
    Task<int> AddCommentAsync(Comment comment,CancellationToken cancellationToken);
}