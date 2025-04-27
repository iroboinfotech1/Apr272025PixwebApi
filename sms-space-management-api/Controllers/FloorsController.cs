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
    public class FloorsController : ControllerBase
    {
        
        private readonly IFloorService _service;
        public FloorsController(IFloorService service)
        {
            this._service = service;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloor()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet("GetFloorByBuilding")]
        public async Task<ActionResult<Floor>> GetFloorByBuilding(int id)
        {
            var response = await _service.GetFloorByBuilding(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> GetFloor(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/Floors/5
        [HttpPut]
        public async Task<IActionResult> PutFloor(Floor floor)
        {
            if (floor.FloorId == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(floor);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
        }

        // POST: api/Floors      
        [HttpPost]
        public async Task<ActionResult<Floor>> PostFloor(Floor floor)
        {
            var response = await _service.Create(floor);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/Floors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFloor(int id)
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
