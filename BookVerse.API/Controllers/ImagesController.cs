using BookVerse.API.Services;
using BookVerse.Domain.Entities;
using BookVerse.Infrastructure.Dbcontext;
using Microsoft.AspNetCore.Mvc;
using BookVerse.API.Models;
using BookVerse.Application.DTOs.Images;
using Microsoft.EntityFrameworkCore;

namespace BookVerse.API.Controllers;

[ApiController]
[Route("api/books/{bookId}/images")]
public class ImagesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    private readonly CloudinaryService _cloudinaryService;

    public ImagesController(
        ApplicationDbContext context,
        CloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    [HttpPost]
public async Task<IActionResult> UploadImage(
    Guid bookId,
    [FromForm] UploadImageRequest request)
{
    var bookExists = await _context.Books.FindAsync(bookId);

    if (bookExists == null)
        return NotFound("Book not found");

    // CHECK FILE NULL
    if (request.File == null || request.File.Length == 0)
    {
        return BadRequest("No file uploaded");
    }

    // VALIDATE FILE SIZE
    const long maxFileSize = 10 * 1024 * 1024;

    if (request.File.Length > maxFileSize)
    {
        return BadRequest(
            "File size cannot exceed 10MB"
        );
    }

    // UPLOAD CLOUDINARY
    var uploadResult =
    await _cloudinaryService.UploadImageAsync(
        request.File
    );

    var image = new BookImage
    {
        Id = Guid.NewGuid(),
        ImageUrl = uploadResult.ImageUrl,
        BookId = bookId,
        PublicId = uploadResult.PublicId
    };

    await _context.BookImages.AddAsync(image);

    await _context.SaveChangesAsync();

    return Ok(new ImageResponseDto
{
    Id = image.Id,
    ImageUrl = image.ImageUrl,
    BookId = image.BookId
});
}

[HttpDelete("{imageId}")]
public async Task<IActionResult> DeleteImage(
    Guid bookId,
    Guid imageId)
{
    var image = await _context.BookImages
        .FirstOrDefaultAsync(x =>
            x.Id == imageId &&
            x.BookId == bookId
        );

    if (image == null)
    {
        return NotFound("Image not found");
    }

    // DELETE CLOUDINARY
    await _cloudinaryService.DeleteImageAsync(
        image.PublicId
    );

    // DELETE DATABASE
    _context.BookImages.Remove(image);

    await _context.SaveChangesAsync();

    return Ok(new
    {
        message = "Image deleted successfully"
    });
}
}