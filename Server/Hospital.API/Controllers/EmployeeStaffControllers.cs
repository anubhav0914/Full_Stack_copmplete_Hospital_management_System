using Microsoft.AspNetCore.Mvc;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    // [Authorize(Roles = "Admin,Patient")]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeStaffServices _services;

        public EmployeeController(IEmployeeStaffServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] EmployeeStaffRequestDTO requestDTO)
        {

            var result = await _services.RegisterEmployee(requestDTO);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }

        [HttpGet]
        [Route("allEmployee")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllEmployee();

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

        public async Task<ActionResult> Update([FromBody] EmployeeStaffRequestDTO requestDTO)
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
        [Route("GetEmployeeWithDepartmentID/{id:int}")]

        public async Task<ActionResult> GetEmployeeWithDepartmentID(int id)
        {
            var result = await _services.GetEmployeeByDepartmentIdAsync(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        
    }
}