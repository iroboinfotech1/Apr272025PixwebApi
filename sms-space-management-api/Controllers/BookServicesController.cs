using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.BookRoom;
using sms.space.management.domain.Entities.PlayerManagement;
using sms.space.management.domain.Entities.UserManagement;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class BookServicesController : ControllerBase
    {

        private readonly IBookRoomServices _service;
        public BookServicesController(IBookRoomServices service)
        {
            this._service = service;
        }



        #region == Add Services  ==

        // GET: api/GetBookServices
        [HttpGet]
        [Route("GetBookServices")]
        public async Task<ActionResult<IEnumerable<ServiceDetail>>> GetBookServices()
        {
            var response = await _service.GetBookServices();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetBookServices/meetingId
        [HttpGet("GetBookServices/{meetingId}")]

        public async Task<ActionResult<List<ServiceDetail>>> GetBookServices(int meetingId)
        {
            var response = await _service.GetBookServices(meetingId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/UpdateBookServices
        [HttpPost]
        [Route("UpdateBookServices")]
        public async Task<IActionResult> UpdateBookServices(List<ServiceDetail> services)
        {
            await _service.UpdateBookServices(services);
            return Ok(new ApiResponse("Updated Successfully", ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostBookServices
        [HttpPost]
        [Route("PostBookServices")]
        public async Task<ActionResult<ServiceDetail>> PostBookServices(ServiceDetail services)
        {
            var response = await _service.CreateBookServices(services);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeleteBookServices/serviceId
        [HttpDelete("DeleteBookServices/{serviceId}")]
        public async Task<IActionResult> DeleteBookServices(int serviceId)
        {
            var response = await _service.DeleteBookServices(serviceId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion




    }
}
