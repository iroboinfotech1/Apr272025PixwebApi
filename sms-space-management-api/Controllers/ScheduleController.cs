using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.Schedule;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service)
        {
            this._service = service;
        }

        #region == Schedule ==

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            var response = await _service.GetSchedules();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

  
        [HttpGet("{scheduleId}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int scheduleId)
        {
            var response = await _service.GetSchedule(scheduleId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpPost]
        public async Task<ActionResult<Category>> CreateSchedule(Schedule schedule)
        {
            var response = await _service.CreateSchedule(schedule);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchedule(Schedule request)
        {
            if (string.IsNullOrEmpty(request.ItemName))
            {
                return BadRequest();
            }
            var response = await _service.UpdateSchedule(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule(Schedule request)
        {
            var response = await _service.DeleteSchedule(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion
    }
}
