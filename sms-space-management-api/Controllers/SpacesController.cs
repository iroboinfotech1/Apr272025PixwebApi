using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.application.Models.Dtos.Spaces;
using sms.space.management.application.Services;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class SpacesController : ControllerBase
    {
        private readonly ISpacesSevice _service;

        public SpacesController(ISpacesSevice service)
        {
            _service = service;
        }

        // GET: api/Spaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpace()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }
        [HttpPost("GetSpacebyFloorId")]
        public async Task<ActionResult<IEnumerable<Space>>> GetSpacebyFloorId(int floorid)
        {
            var response = await _service.GetAllSpacebyFloorId(floorid);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // POST: api/Spaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("GetAvailableSpace")]
        public async Task<ActionResult<IEnumerable<Space>>> GetAvailableSpace(SpaceFilter filter)
        {
            if(filter == null) {
                return Ok(new ApiResponse(null, ApiStatusCodes.InputValidationFailure, false, "Input Error"));
            }
            else
            {
                var response = await _service.GetAvailableSpace(filter);
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
            }
        }

        // GET: api/Spaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Space>> GetSpace(int id)
        {
            string baseUrl = "http://demo.pixelkube.io";

            string endpoint = "/api/pixconnectors/connector/getcalendars";

            string queryParamKey = "connectorName";

            var response = await _service.GetById(id, baseUrl, endpoint, queryParamKey);

            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/Spaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpace(Spaces spaces)
        {
            if (spaces != null && spaces.SpaceId != 0)
            {
                var response = await _service.Update(spaces.SpaceId, spaces);

                if (response)
                    return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }

        // POST: api/Spaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Space>> PostSpace(Spaces spaces)
        {
            var response = await _service.Create(spaces);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/Spaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpace(int id)
        {
            var response = await _service.Delete(id);
            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
            else
                return NotFound();
        }

        [HttpPost("SaveSettings")]
        public async Task<IActionResult> SaveSettings(Settings request)
        {
            var response = await _service.SaveSettings(request);
            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Saved Successfully"));
            else
                return NotFound();
        }


        // GET: api/Rooms
        [HttpPost("FindRooms")]
        public async Task<ActionResult<List<KeyValuePair<string, FloorSpaces>>>> FindRooms(SpaceFilter filter)
        {
            var response = await _service.FindRooms(filter);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


    }
}
