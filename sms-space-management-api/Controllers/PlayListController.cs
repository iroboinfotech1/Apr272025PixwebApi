using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.ContentManagement;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {

        private readonly IPlayListService _service;

        public PlayListController(IPlayListService service)
        {
            _service = service;
        }

        // GET: api/GetPlayList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayList>>> GetPlayList()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        /*// GET: api/GetPlayList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayList>> GetPlayList(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }*/

        // GET: api/GetPlayList/5
        [HttpGet("{name}")]
        public async Task<ActionResult<PlayList>> GetPlayList(string name)
        {
            var response = await _service.GetByPlayListName(name);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/PutPlayList
        [HttpPut]
        public async Task<IActionResult> PutPlayList(List<PlayList> playList)
        {
            if (playList == null || playList.Count == 0)
            {
                return BadRequest();
            }
            var response = await _service.Update(playList);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }

        // POST: api/PostPlayList
        [HttpPost]
        public async Task<ActionResult<PlayList>> PostPlayList(PlayList playList)
        {
            var response = await _service.Create(playList);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpDelete("{playListName}")]
        public async Task<IActionResult> DeletePlayList(string playListName)
        {
            var response = await _service.Delete(playListName);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        // DELETE: api/DeletePlayListItem/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayListItem(int id)
        {
            var response = await _service.DeletePlayListItem(id);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }
    }
}
