using BookVerse.Application.DTOs.Reviews;
using BookVerse.Domain.Entities;
using BookVerse.Infrastructure.Dbcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookVerse.Application.Models;
namespace BookVerse.API.Controllers;

[ApiController]
[Route("api/books/{bookId}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReviewsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET reviews by book
    [HttpGet]
    public async Task<IActionResult> GetReviews(Guid bookId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.BookId == bookId)
            .Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                ReviewContent = r.ReviewContent
            })
            .ToListAsync();

        return Ok(ApiResult<List<ReviewResponseDto>>.Success(reviews));
    }

    // POST review
    [HttpPost]
    public async Task<IActionResult> CreateReview(
        Guid bookId,
        CreateReviewDto dto)
    {
        var bookExists = await _context.Books
            .AnyAsync(b => b.Id == bookId);

        if (!bookExists)
            return NotFound("Book not found");

        var review = new Review
        {
            Id = Guid.NewGuid(),
            ReviewerName = dto.ReviewerName,
            ReviewContent = dto.ReviewContent,
            BookId = bookId
        };

        await _context.Reviews.AddAsync(review);

        await _context.SaveChangesAsync();

        return Ok(review);
    }
}