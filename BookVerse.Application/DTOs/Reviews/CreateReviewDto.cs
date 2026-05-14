namespace BookVerse.Application.DTOs.Reviews;

public class CreateReviewDto
{
    public string ReviewerName { get; set; } = string.Empty;

    public string ReviewContent { get; set; } = string.Empty;
}