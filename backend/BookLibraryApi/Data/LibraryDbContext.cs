using BookLibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApi.Data;

public class LibraryDbContext:DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOptions):base(dbContextOptions)
    {
        
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Comment> Comments { get; set; }
}