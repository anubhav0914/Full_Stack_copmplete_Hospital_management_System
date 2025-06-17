using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Hospital.Business.Cloudnary;
using Hospital.Business.Services.ImageService;
using Hospital.Bussiness.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hospital.Business.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(UploadImageRequest file)
        {
            if (file.File.Length == 0)
                throw new ArgumentException("File is empty");

            await using var stream = file.File.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.File.Name, stream),
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                return uploadResult.SecureUrl.ToString();

            throw new Exception("Image upload failed.");
        }
    }
}
