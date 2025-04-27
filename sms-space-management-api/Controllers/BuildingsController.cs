using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.Building;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _service;
        public BuildingsController(IBuildingService service)
        {
            this._service = service;
        }



        [HttpPost]
        [Route("CreateBuilding")]
        public async Task<IActionResult> Create(Building request)
        {
            var response = await _service.Create(request);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpGet]
        [Route("getBuildingsbyOrg")]
        public async Task<IActionResult> GetBuildingsByOrg(int id)
        {
            var response = await _service.GetBuildingsByOrg(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet]
        [Route("getListOfBuildings")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // DELETE: api/building/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            var response = await _service.Delete(id);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBuilding(Building request)
        {
            if (request.BuildingId == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
        }

    }
}
