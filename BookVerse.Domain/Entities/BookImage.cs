namespace BookVerse.Domain.Entities;

public class BookImage
{
    public Guid Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public Guid BookId { get; set; }

    public Book? Book { get; set; }

    public string PublicId { get; set; } = string.Empty;
}