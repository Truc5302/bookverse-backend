using BookVerse.Application.DTOs.Images;
using BookVerse.Application.DTOs.Reviews;

namespace BookVerse.Application.DTOs.Books;

public class BookDetailDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public string Content { get; set; } = string.Empty;

    public List<ReviewResponseDto> Reviews { get; set; }
        = new();

    public List<BookImageDto> Images { get; set; }
        = new();
}