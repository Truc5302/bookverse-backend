namespace BookVerse.Application.DTOs.Images;

public class ImageResponseDto
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid BookId { get; set; }
}