namespace BookVerse.Application.DTOs.Books;

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public string Content { get; set; } = string.Empty;
}
