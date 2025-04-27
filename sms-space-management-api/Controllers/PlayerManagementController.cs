using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.application.Models.Dtos.Spaces;
using sms.space.management.domain.Entities.PlayerManagement;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class PlayerManagementController : ControllerBase
    {
        private readonly IPlayerManagementService _service;

        private readonly ISpacesSevice _service1;


        //public PlayerManagementController(IPlayerManagementService service)
        //{
        //    this._service = service;
        //}

        public PlayerManagementController(IPlayerManagementService service, ISpacesSevice service1)
        {
            this._service = service;
            this._service1 = (ISpacesSevice)service1;
        }

        // GET: api/GetPlayerManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerManagement>>> GetPlayerManagement()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/GetPlayerManagement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerManagement>> GetPlayerManagement(string id)
        {

            var response = await _service.GetBySerialNumber(id);

            string baseUrl = "https://demo.pixelkube.io/";

            // Replace with the actual endpoint
            string endpoint = "/api/pixconnectors/connector/getcalendars";

            // Replace with your actual query parameter and value
            string queryParamKey = "connectorName";

            var spaceresponse = await _service1.GetById(response.SpaceId, baseUrl,endpoint,queryParamKey);

            if(spaceresponse!=null && response!=null)
            {
                GetSpacesDto obj = (GetSpacesDto)spaceresponse;
                PlayerManagement player= (PlayerManagement)response;
                player.SpaceId= obj.SpaceId;
                player.ConnectorId=obj?.MappedConnectorIds?.FirstOrDefault();

                player.CalendarId=obj?.MappedCalendarIds?.FirstOrDefault();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutPlayerManagement
        [HttpPut]
        public async Task<IActionResult> PutPlayerManagement(PlayerManagement playerManagement)
        {
            if (string.IsNullOrEmpty(playerManagement.SerialNumber))
            {
                return BadRequest();
            }
            var response = await _service.Update(playerManagement);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostPlayerManagement
        [HttpPost]
        public async Task<ActionResult<PlayerManagement>> PostPlayerManagement(PlayerManagement playerManagement)
        {
            var response = await _service.Create(playerManagement);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        // DELETE: api/DeletePlayerManagement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerManagement(string id)
        {
            var response = await _service.Delete(id);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        [HttpGet("RetrievePlayerSensitiveInformation")]
        public async Task<ActionResult<PlayerSensitive>> RetrievePlayerSensitiveInformation(string serialNo)
        {
            var response = await _service.RetrievePlayerSensitiveInformation(serialNo);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpPost("InsertPlayerSensitiveInformation")]
        public async Task<IActionResult> InsertPlayerSensitiveInformation(PlayerSensitive request)
        {
            var response = await _service.InsertPlayerSensitiveInformation(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }




        [HttpPost("ValidatePlayerSensitiveInformation")]
        public async Task<bool> ValidatePlayerSensitiveInformation(PlayerSensitive request)
        {
            var response = await _service.RetrievePlayerSensitiveInformation(request.SerialNumber);
            if (response != null && response.SerialNumber != null 
                 && request.SerialNumber.Equals(response.SerialNumber,StringComparison.InvariantCultureIgnoreCase) 
                 && request.SixDigitCode.Equals(response.SixDigitCode,StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        [HttpPost("InsertPlayerLogs")]
        public async Task<IActionResult> InsertPlayerLogs(PlayerLogs request)
        {
            var response = await _service.InsertPlayerLogs(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));

        }

        [HttpGet("GetPlayerLogsBySerialNumber")]
        public async Task<ActionResult<PlayerLogs>> GetPlayerLogsBySerialNumber(string serialNo)
        {
            var response = await _service.GetPlayerLogsBySerialNumber(serialNo);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

     }
}
