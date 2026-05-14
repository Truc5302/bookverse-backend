using BookVerse.API.Configurations;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using BookVerse.API.Models;

namespace BookVerse.API.Services;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(
        IOptions<CloudinarySettings> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<CloudinaryUploadResult> UploadImageAsync(
    IFormFile file)
{
    await using var stream = file.OpenReadStream();

    var uploadParams = new ImageUploadParams
    {
        File = new FileDescription(
            file.FileName,
            stream
        ),

        Folder = "bookverse"
    };

    var uploadResult =
        await _cloudinary.UploadAsync(uploadParams);

    if (uploadResult.Error != null)
    {
        throw new Exception(uploadResult.Error.Message);
    }

    return new CloudinaryUploadResult
    {
        ImageUrl = uploadResult.SecureUrl.ToString(),
        PublicId = uploadResult.PublicId
    };
}

public async Task DeleteImageAsync(string publicId)
{
    var deleteParams =
        new DeletionParams(publicId);

    var result =
        await _cloudinary.DestroyAsync(deleteParams);

    if (result.Result != "ok")
    {
        throw new Exception(
            "Failed to delete image"
        );
    }
}
}