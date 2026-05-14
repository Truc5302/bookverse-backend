using BookVerse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.Infrastructure.Dbcontext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<BookImage> BookImages => Set<BookImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Reviews)
            .WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Images)
            .WithOne(i => i.Book)
            .HasForeignKey(i => i.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}