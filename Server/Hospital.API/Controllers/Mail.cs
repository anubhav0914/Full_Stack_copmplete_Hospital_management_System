using Hpospital.Bussiness.Services.MailServices;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMailService _mailService;

        public ContactController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromQuery] string email)
        {
            await _mailService.SendEmailPatientAsync(email,"Anubhav");
            return Ok("Email sent successfully.");
        }
    }
}
