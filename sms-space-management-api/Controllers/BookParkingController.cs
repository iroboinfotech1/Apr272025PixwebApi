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
    public class BookParkingController : ControllerBase
    {

        private readonly IBookParkingServices _service;
        public BookParkingController(IBookParkingServices service)
        {
            this._service = service;
        }



        #region == Add Parking  ==

        // GET: api/GetBookParking
        [HttpGet]

        public async Task<ActionResult<IEnumerable<BookParking>>> GetBookParking()
        {
            var response = await _service.GetBookParking();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        // GET: api/GetBookParking/parkingId
        [HttpGet("{meetingId}")]

        public async Task<ActionResult<Parkings>> GetBookParking(int meetingId)
        {
            var response = await _service.GetBookParking(meetingId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutBookParking
        [HttpPut]

        public async Task<IActionResult> PutBookParking(BookParking parking)
        {
            if (string.IsNullOrEmpty(parking.SlotDetails))
            {
                return BadRequest();
            }
            var response = await _service.UpdateBookParking(parking);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostBookParking
        [HttpPost]

        public async Task<ActionResult<Parkings>> PostBookParking(Parkings parking)
        {
            var response = await _service.CreateBookParking(parking);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeleteBookParking/parkingId
        [HttpDelete("{parkingId}")]
        public async Task<IActionResult> DeleteBookParking(int parkingId)
        {
            var response = await _service.DeleteBookParking(parkingId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion




    }
}
