using BookLibraryApi.Models;
using BookLibraryApi.Models.Responses;

namespace BookLibraryApi.Interfaces.Services;

public interface IBookService
{
    Task<GetBooksResponse> GetBooksAsync(int page,int pageSize,string search);
    Task<IEnumerable<Comment>> GetCommentsAsync(int bookId);
    Task<int?> AddCommentAsync(int bookId,string commment);

}