using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class DoctorController : Controller
    {

        private readonly IDoctorServices _services;

        public DoctorController(IDoctorServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] DoctorRequestDTO requestDTO)
        {

            var result = await _services.RegisterDoctor(requestDTO);

            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("allDoctors")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllDoctors();

            return result.Status ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetById/{id:int}")]

        public async Task<ActionResult> GetById(int id)
        {
            var result = await _services.GetById(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("update")]

        public async Task<ActionResult> Update([FromBody] DoctorRequestDTO requestDTO)
        {
            var result = await _services.Update(requestDTO);
            return result.Status ? Ok(result) : BadRequest(result);
        }        
        [HttpGet]
        [Route("GetDoctorWithDepartmentID/{id:int}")]

        public async Task<ActionResult> GetDoctorWithDepartmentID(int id)
        {
            var result = await _services.GetDoctorsByDepartmentIdAsync(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
    }
}