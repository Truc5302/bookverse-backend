namespace BookVerse.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ICollection<Review> Reviews { get; set; }
        = new List<Review>();

    public ICollection<BookImage> Images { get; set; }
        = new List<BookImage>();
}