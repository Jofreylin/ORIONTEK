using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORION_BACKEND.DTO;
using ORION_BACKEND.Models;
using ORION_BACKEND.Services;

namespace ORION_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<Customer>>> GetList()
        {
            var result = await _repo.GetList();
            return Ok(result);
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            var result = await _repo.GetById(id);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<ReturnModel>> Insert(CustomerDTO model)
        {
            var result = await _repo.Insert(model);
            return Ok(result);  
        }

        [HttpPut()]
        public async Task<ActionResult<ReturnModel>> Update(CustomerDTO model)
        {
            var result = await _repo.Update(model);
            return Ok(result);
        }
    }
}
