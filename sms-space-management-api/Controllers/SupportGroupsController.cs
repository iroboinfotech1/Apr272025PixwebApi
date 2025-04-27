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
    public class SupportGroupsController : ControllerBase
    {
        
        private readonly ISupportGroupService _service;
        public SupportGroupsController(ISupportGroupService service)
        {
            this._service = service;
        }

        // GET: api/GetSupportGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupportGroup>>> GetSupportGroup()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetSupportGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupportGroup>> GetSupportGroup(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutSupportGroup/supportGroup
        [HttpPut]
        public async Task<IActionResult> PutSupportGroup(SupportGroup supportGroup)
        {
            if (supportGroup.SupportGroupID == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(supportGroup);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostSupportGroup
        [HttpPost]
        public async Task<ActionResult<SupportGroup>> PostSupportGroup(SupportGroup supportGroup)
        {
            var response = await _service.Create(supportGroup);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeleteSupportGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportGroup(int id)
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
