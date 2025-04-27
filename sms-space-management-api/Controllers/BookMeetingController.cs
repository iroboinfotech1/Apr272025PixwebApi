using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookDesk;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class BookMeetingController : ControllerBase
    {
        private readonly IBooKMeetingService _service;

        public BookMeetingController(IBooKMeetingService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<List<BookedMeetingDetail>>> GetBookMeeting(int spaceId, DateTime startDate, DateTime endDate)
        {
            var response = await _service.GetBookMeeting(spaceId, startDate,  endDate);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpGet("{meetingId}")]
        public async Task<ActionResult<BookMeetingDetails>> GetBookMeetingByMeetingId(int meetingId)
        {
            var response = await _service.GetBookMeeting(meetingId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpPut]
        public async Task<IActionResult> PutBookMeeting(BookMeetingDetails bookMeeting)
        {
            if (bookMeeting != null && bookMeeting.Meeting != null && bookMeeting.Meeting.MeetingId != 0)
            {
                var response = await _service.UpdateMeeting(bookMeeting);

                if (response)
                    return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }


        [HttpPost]
        public async Task<ActionResult<Space>> PostBookMeeting(BookMeetingDetails bookMeeting)
        {
            var response = await _service.CreateMeeting(bookMeeting);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }


        // POST: api/BookMeeting
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateDeskBooking")]
        public async Task<ActionResult<BookDeskDetails>> CreateDeskBooking(BookDeskDetails bookDesk)
        {
            var response = await _service.CreateDeskBooking(bookDesk);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }


        [HttpDelete("{meetingId}")]
        public async Task<IActionResult> DeleteBookMeetingById(int meetingId)
        {
            var response = await _service.DeleteMeeting(meetingId);
            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
            else
                return NotFound();
        }

    }
}
