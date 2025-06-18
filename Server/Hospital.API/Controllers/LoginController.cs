using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Bussiness.Services.AuthServices;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
namespace Hospital.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class LoginController : Controller
    {
        private readonly ILoginServices _services;
        public LoginController(ILoginServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var result = await _services.Login(loginRequestDTO);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("Adminlogin")]
        public async Task<ActionResult> AdminLogin([FromBody] AdminloginDTO loginRequestDTO)
        {
            var result = await _services.AdminLogin(loginRequestDTO);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("AdminloginOTPVerification")]
        public async Task<ActionResult> AdminLoginOtpVerification([FromBody] OTPDTO otpdtp)
        {
            var result = await _services.AdminLoginOtpVerification(otpdtp);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}