using BookVerse.Application.DTOs.Books;
using BookVerse.Domain.Entities;
using BookVerse.Infrastructure.Dbcontext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookVerse.Application.DTOs.Reviews;
using BookVerse.Application.DTOs.Images;
using BookVerse.Application.Models;

namespace BookVerse.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Books
            .Select(book => new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Content = book.Content
            })
            .ToListAsync();

        return Ok(ApiResult<List<BookResponseDto>>.Success(books));
    }

    // GET: api/books/{id}
    [HttpGet("{id}")]
public async Task<IActionResult> GetBook(Guid id)
{
    var book = await _context.Books
        .Include(b => b.Reviews)
        .Include(b => b.Images)
        .Where(b => b.Id == id)
        .Select(b => new BookDetailDto
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Year = b.Year,
            Content = b.Content,

            Reviews = b.Reviews.Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                ReviewContent = r.ReviewContent
            }).ToList(),

            Images = b.Images.Select(i => new BookImageDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (book == null)
        return NotFound();

    return Ok(ApiResult<BookDetailDto>.Success(book));
}

    // POST: api/books
    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookDto dto)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Author = dto.Author,
            Year = dto.Year,
            Content = dto.Content
        };

        await _context.Books.AddAsync(book);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, UpdateBookDto dto)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Year = dto.Year;
        book.Content = dto.Content;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/books/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _context.Books.Remove(book);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}