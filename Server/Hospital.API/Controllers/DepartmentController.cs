using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
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
    public class DepartmentController : Controller
    {

        private readonly IDepartmentServices _services;

        public DepartmentController(IDepartmentServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("AddDepartment")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromBody] DepartmentRequestDTO requestDTO)
        {

            var result = await _services.AddDepartment(requestDTO);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }

        [HttpGet]
        [Route("allDepartment")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllDepartment();

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

        public async Task<ActionResult> Update([FromBody] DepartmentRequestDTO requestDTO)
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
        
    }
}