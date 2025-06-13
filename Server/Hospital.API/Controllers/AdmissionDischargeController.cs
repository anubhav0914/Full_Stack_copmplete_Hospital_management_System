using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Hospital.Bussiness.Services;

namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class AdmissionDischargeController : Controller
    {

        private readonly IAdmissionDischargeServices _services;

        public AdmissionDischargeController(IAdmissionDischargeServices services)
        {

            _services = services;
        }

        [HttpGet]
        [Route("getAllDeatilOfAdmissionDischarge")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _services.GetAllAdmissionDischarge();

            return result.Status ? Ok(result) : BadRequest(result);

        }
      

    }
}