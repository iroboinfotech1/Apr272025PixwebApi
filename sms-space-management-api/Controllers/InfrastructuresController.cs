using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.Building;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class InfrastructuresController : ControllerBase
    {
        private readonly IInfrastructureService _service;
        public InfrastructuresController(IInfrastructureService service)
        {
            this._service = service;
        }

        // GET: api/Infrastructures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infrastructure>>> GetInfrastructure()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/Infrastructures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Infrastructure>> GetInfrastructure(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/Infrastructures/5
        [HttpPut]
        public async Task<IActionResult> PutInfrastructure(Infrastructure infrastructure)
        {
            if (infrastructure.infra_id == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(infrastructure);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
        }

        // POST: api/Infrastructures      
        [HttpPost]
        public async Task<ActionResult<Infrastructure>> PostInfrastructure(Infrastructure infrastructure)
        {
            var response = await _service.Create(infrastructure);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/Infrastructures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfrastructure(int id)
        {
            var response = await _service.Delete(id);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }
    }
}
