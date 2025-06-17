using Hospital.Business.Cloudnary;
using Hospital.Business.Services.ImageService;
using Hospital.Bussiness.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> UploadImage([FromForm] UploadImageRequest file)
        {
            var url = await _imageService.UploadImageAsync(file);
            return Ok(new { imageUrl = url });
        }
    }
}
