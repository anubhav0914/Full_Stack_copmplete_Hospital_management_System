using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Bussiness.DTOs
{
    
public class UploadImageRequest
{
    [Required]
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}
}
