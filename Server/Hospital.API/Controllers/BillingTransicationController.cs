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
    public class BillingTransactionController : Controller
    {

        private readonly IBillingTransicationServices _services;

        public BillingTransactionController(IBillingTransicationServices services)
        {

            _services = services;
        }

        [HttpPost]
        [Route("AddNewBill")]
        public async Task<IActionResult> AddNewBill([FromBody] BillingTransactionRequestDTO requestDTO)
        {

            var result = await _services.AddNewBill(requestDTO);

            return result.Status ? Ok(result) : BadRequest(result);
            return Ok();
        }

        [HttpGet]
        [Route("allBills")]
        public async Task<IActionResult> GetAll()
        {

            var result = await _services.GetAllBills();

            return result.Status ? Ok(result) : BadRequest(result);

        }

        [HttpGet]
        [Route("GetById/{id:int}")]

        public async Task<ActionResult> GetById(int id)
        {
            var result = await _services.GetById(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpGet]
        [Route("GetBillByDoctorId/{id:int}")]

        public async Task<ActionResult> GetBillByDoctorID(int id)
        {
            var result = await _services.GetBillByDoctorID(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        [Route("GetBillByPatientId/{id:int}")]

        public async Task<ActionResult> GetBillByPatientID(int id)
        {
            var result = await _services.GetBillByPatientID(id);
            return result.Status ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        [Route("GetBillByDate")]

        public async Task<ActionResult> GetBillByDate([FromBody] DateTime date)
        {
            var result = await _services.GetBillByDate(date);
            return result.Status ? Ok(result) : BadRequest(result);
        }

    }
}