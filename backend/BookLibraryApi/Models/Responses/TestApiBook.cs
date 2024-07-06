namespace BookLibraryApi.Models.Responses;

public class TestApiBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publication_year { get; set; }
    public List<string> Genre { get; set; }
    public string Description { get; set; }
    public string Cover_image { get; set; }
}