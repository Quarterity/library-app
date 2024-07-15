using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;

namespace BookLibraryApi.Interfaces.Services;

public interface IBookService
{
    Task<GetBooksResponse> GetBooksAsync(int page,int pageSize,string search,CancellationToken cancellationToken);
    Task<IEnumerable<Comment>> GetCommentsAsync(int bookId,CancellationToken cancellationToken);
    Task<int> AddCommentAsync(int bookId,string comment,CancellationToken cancellationToken);

}