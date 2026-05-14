namespace BookVerse.Application.DTOs.Books;

public class BookResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public string Content { get; set; } = string.Empty;
}