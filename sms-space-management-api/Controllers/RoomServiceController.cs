using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.RoomService;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class RoomServiceController : ControllerBase
    {
        private readonly IRoomService _service;
        public RoomServiceController(IRoomService service)
        {
            this._service = service;
        }


        #region == RoomService ==


        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomServiceDetail>>> GetRoomServices()
        {
            var response = await _service.GetRoomServices();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpGet("{roomServiceId}")]
        public async Task<ActionResult<RoomServiceDetail>> GetRoomService(int roomServiceId)
        {
            var response = await _service.GetRoomService(roomServiceId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRoomService(RoomServiceDetail request)
        {
            if (string.IsNullOrEmpty(request.ItemName))
            {
                return BadRequest();
            }
            var response = await _service.UpdateRoomService(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }


        [HttpPost]
        public async Task<ActionResult<RoomServiceDetail>> CreateRoomService(RoomServiceDetail request)
        {
            var response = await _service.CreateRoomService(request);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }


        [HttpDelete("{roomServiceId}")]
        public async Task<IActionResult> DeleteRoomService(int roomServiceId)
        {
            var response = await _service.DeleteRoomService(roomServiceId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion
    }
}
