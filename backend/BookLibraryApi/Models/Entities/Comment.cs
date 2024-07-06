namespace BookLibraryApi.Models;

public class Comment
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Book Book { get; set; }
}