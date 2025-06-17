using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Hospital.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class AppointmenttController : Controller
    {

        private readonly IAppointmentServices _services;

        public AppointmenttController(IAppointmentServices services)
        {

            _services = services;
        }

        [Authorize(Roles = "patient")]
        [HttpPost]
        [Route("AddAppointment")]
        
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentRequestDTO requestDTO)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _services.AddAppointment(requestDTO, email);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }

        [HttpGet]
        [Route("allAppointments")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllAppointment();

            return result.Status ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        [Authorize(Roles = "admin,patient")]


        public async Task<ActionResult> GetById(int id)
        {
            var result = await _services.GetById(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        [Route("update")]

        public async Task<ActionResult> Update([FromBody] AppointmentRequestDTO requestDTO)
        {
            var result = await _services.Update(requestDTO);
            return result.Status ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("delete/{id:int}")]
        [Authorize(Roles = "patient")]


        public async Task<ActionResult> Delete(int id)
        {
            var result = await _services.Delete(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }


        [HttpGet]
        [Route("GetAppointmentByDoctorID/{id:int}")]
        [Authorize(Roles = "admin")]


        public async Task<ActionResult> GetAppointmentByDoctorID(int id)
        {
            var result = await _services.GetAppointmentByDoctorID(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("GetAppointmentByPatientID/{id:int}")]
        [Authorize(Roles = "patient")]


        public async Task<ActionResult> GetAppointmentByPatientID(int id)
        {
            var result = await _services.GetAppointmentByPatientID(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        [Route("getAppointmentByDate")]
        [Authorize(Roles = "admin")]
        
        public async Task<IActionResult> GetAppointmentByDate([FromBody] DateTime date)
        {

            var result = await _services.GetAppointmentByDate(date);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }


    }
}