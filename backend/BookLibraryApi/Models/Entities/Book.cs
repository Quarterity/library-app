namespace BookLibraryApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string PublicationYear { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }
    public ICollection<Comment> Comments { get; set; }
}