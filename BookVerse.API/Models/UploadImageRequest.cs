using Microsoft.AspNetCore.Http;

namespace BookVerse.API.Models;

public class UploadImageRequest
{
    public IFormFile File { get; set; } = default!;
}