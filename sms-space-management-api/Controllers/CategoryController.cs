using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.Category;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            this._service = service;
        }


        #region == Category ==


        [HttpGet]
        [Route("GetCategorys")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategorys()
        {
            var response = await _service.GetCategorys();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpGet("{categoryTxnId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryTxnId)
        {
            var response = await _service.GetCategory(categoryTxnId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Category request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest();
            }
            var response = await _service.UpdateCategory(request);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));

        }


        [HttpPost]
        [Route("CreateCategory")]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var response = await _service.CreateCategory(category);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(Category category)
        {
            var response = await _service.DeleteCategory(category);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        #endregion
    }
}
