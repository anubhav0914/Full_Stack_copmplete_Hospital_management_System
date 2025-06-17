using Hospital.Bussiness.DTOs;
using Microsoft.AspNetCore.Http;

namespace Hospital.Business.Services.ImageService
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(UploadImageRequest file);
    }
}
