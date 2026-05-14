namespace BookVerse.Application.DTOs.Reviews;

public class ReviewResponseDto
{
    public Guid Id { get; set; }

    public string ReviewerName { get; set; } = string.Empty;

    public string ReviewContent { get; set; } = string.Empty;
}