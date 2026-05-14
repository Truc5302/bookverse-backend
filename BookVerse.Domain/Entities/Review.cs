namespace BookVerse.Domain.Entities;

public class Review
{
    public Guid Id { get; set; }

    public string ReviewerName { get; set; } = string.Empty;

    public string ReviewContent { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int Rating { get; set; }

    // Foreign Key
    public Guid BookId { get; set; }

    // Navigation Property
    public Book? Book { get; set; }
}