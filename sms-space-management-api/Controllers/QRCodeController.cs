using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.application.Services;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.QRCode;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _service;
        private readonly DataBaseContext _context;
        private readonly JwtService _jwtService;

        public QRCodeController(DataBaseContext context, IQRCodeService service, JwtService jwtService)
        {
            _context = context;
            _service = service;
            _jwtService = jwtService;
        }

        // POST: api/SMSService/QRCode/SaveQRCode
        [HttpPost("SaveQRCode")]
        public async Task<ActionResult<bool>> SaveQRCode(qrcodedetail request)
        {
            request.QrToken = _jwtService.GenerateToken(request.UserName, request.RoomId, request.UserId);
            var response = await _service.SaveQRCode(request);
            return Ok(response);
        }

        // GET: api/SMSService/QRCode/GenerateQRCode
        [HttpGet("GenerateQRCode")]
        public async Task<ActionResult<qrcodedetail>> GenerateQRCode([FromQuery] qrcodedetail request)
        {
            var response = await _service.GenerateQRCode(request);
            response.QrToken = _jwtService.GenerateToken(request.UserName, request.RoomId, request.UserId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/SMSService/QRCode/ValidateQRCode
        [HttpGet("ValidateQRCode")]
        public async Task<ActionResult<qrcodedetail>> ValidateQRCode([FromQuery] qrcodedetail request)
        {
            var response = await _service.ValidateQRCode(request);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }
    }
}
