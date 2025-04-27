using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.ReportFault;

namespace sms.space.management.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportFaultController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IReportFaultService _service;
        public ReportFaultController(DataBaseContext context, IReportFaultService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/GetReportFault
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFault>>> GetReportFault()
        {
            var response = await _service.GetReportFaults();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetReportFault/admin
        [HttpGet("{reportFaultId}")]
        public async Task<ActionResult<ReportFault>> GetReportFault(int reportFaultId)
        {
            var response = await _service.GetReportFault(reportFaultId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/ReportFault
        [HttpPut]
        public async Task<IActionResult> PutReportFault(ReportFault reportFault)
        {
            var response = await _service.UpdateReportFault(reportFault);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostReportFault
        [HttpPost]
        public async Task<ActionResult<ReportFault>> PostReportFault(ReportFault reportFault)
        {
            var response = await _service.CreateReportFault(reportFault);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/ReportFault/reportFaultId
        [HttpDelete("{reportFaultId}")]
        public async Task<IActionResult> DeleteReportFault(int reportFaultId)
        {
            var response = await _service.DeleteReportFault(reportFaultId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        // GET: api/GetReportFault
        [HttpGet("LookupFaultReports")]
        public async Task<ActionResult<IEnumerable<LookupReportFault>>> GetLookupFaultReports()
        {
            var response = await _service.GetLookupFaultReports();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

    }
    }

