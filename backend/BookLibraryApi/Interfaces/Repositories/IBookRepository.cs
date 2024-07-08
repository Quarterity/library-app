using BookLibraryApi.Models;

namespace BookLibraryApi.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetBooksAsync(int page,int pageSize,string search);
    Task<int> GetBooksCountAsync(string searchParam);

    Task<Book> GetBookByIdAsync(int id);
    Task<IEnumerable<Comment>> GetCommentsAsync(int bookId);
    Task<int?> AddComment(Comment comment);
}