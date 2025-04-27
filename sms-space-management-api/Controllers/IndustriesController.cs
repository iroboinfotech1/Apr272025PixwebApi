using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        
        private readonly IIndustryService _service;

        public IndustriesController(IIndustryService service)
        {
            this._service = service;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Industry>>> GetIndustries()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Industry>> GetIndustryById(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        // PUT: api/Industries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndustry(Industry industry)
        {
            if (industry.industry_id == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(industry);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
        }

        [HttpPost]
        public async Task<ActionResult<Industry>> PostIndustry(Industry industry)
        {
            var response = await _service.Create(industry);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/Industries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustry(int ID)
        {
            var response = await _service.Delete(ID);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }
       
    }
}
