using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class PatientController : Controller
    {

        private readonly IPatientServices _services;

        public PatientController(IPatientServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm]PatientRequestDTO requestDTO)
        {

            var result = await _services.Register(requestDTO);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }

        [HttpGet]
        [Route("allPatients")]

        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllPatient();

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

        public async Task<ActionResult> Update([FromForm]  PatientRequestDTO requestDTO)
        {
            var result = await _services.Update(requestDTO);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("Delete/{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _services.Delete(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("getByEmail/{email}")]

        public async Task<ActionResult> Getbyemail(string email)
        {
            var result = await _services.GetByEmail(email);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        

    }
}