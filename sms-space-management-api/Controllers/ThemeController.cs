using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.Theme;
using System;
using System.Security.Policy;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace sms.space.management.api.Controllers
{

    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _service;
        private IWebHostEnvironment _environment;
        public ThemeController(IThemeService service, IWebHostEnvironment environment)
        {
            this._service = service;
            this._environment = environment;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ManageTheme>>> GetAll()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<PlayList>> GetById(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet("GetAllLanguages")]
        public async Task<ActionResult<IReadOnlyList<ThemeLanguage>>> GetAllLanguages()
        {
            var response = await _service.GetAllLanguages();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }
        [HttpGet("GetThemeUtilityData")]
        public async Task<ActionResult<IReadOnlyList<ThemeFont>>> GetThemeFont()
        {
            var response = await _service.GetThemeFont();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

    [HttpPost]
        public async Task<ActionResult<ManageTheme>> Create(ManageTheme managetheme)
        {
            var response = await _service.Create(managetheme);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ManageTheme manageTheme)
        {
            if (manageTheme == null)
            {
                return BadRequest();
            }
            var response = await _service.Update(manageTheme);

            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            StringBuilder sb = new StringBuilder();
            var url = "";
            try
            {
                sb.AppendLine("Upload");
                //string applicationBaseUrl = "https://localhost:7177/";
                string applicationBaseUrl = "https://demo.pixelkube.io/";
                if (file == null || file.Length == 0)
                {
                    return BadRequest();
                }

                var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
                
                sb.AppendLine("fileName: " + fileName);

                //var directoryPath = Path.Combine(_environment.ContentRootPath, @"Assets\Images\Themes\Logo");
                var directoryPath = Path.Combine(_environment.ContentRootPath, "MyStaticFiles");

                sb.AppendLine("directoryPath: " + directoryPath);
                
                var filePath = Path.Combine(directoryPath, fileName);
                
                sb.AppendLine("filePath: " + filePath);
                
                if (!Directory.Exists(directoryPath))
                {
                    sb.AppendLine("Create directoryPath");
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = System.IO.File.Create(filePath))
                {
                    sb.AppendLine("Create File");
                    await file.CopyToAsync(stream);
                }


                //url = $"{applicationBaseUrl}Assets/Images/Themes/Logo/{fileName}";
                url = $"{applicationBaseUrl}StaticFiles/{fileName}";

                sb.AppendLine("url: " + url);
                sb.AppendLine("contentRootPath: " + _environment.ContentRootPath);
                sb.AppendLine("webRootPath: " + _environment.WebRootPath);


                return Ok(new ApiResponse(new { url }, ApiStatusCodes.Success, true, "Uploaded Successfully. " + sb.ToString()));
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse(new { url }, ApiStatusCodes.Success, false, "Uploaded Failed"));
            }
        }


        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    var url = "";
        //    try
        //    {
        //        //string applicationBaseUrl = "https://localhost:7177/";
        //        string applicationBaseUrl = "https://demo.pixelkube.io/";
        //        if (file == null || file.Length == 0)
        //        {
        //            return BadRequest();
        //        }

        //        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

        //        var directoryPath = Path.Combine(_environment.ContentRootPath, @"Assets\Images\Themes\Logo");
        //        var filePath = Path.Combine(directoryPath, fileName);

        //        if (!Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);
        //        }

        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            await file.CopyToAsync(stream);
        //        }

        //        url = $"{applicationBaseUrl}Assets/Images/Themes/Logo/{fileName}";

        //        return Ok(new ApiResponse(new { url }, ApiStatusCodes.Success, true, "Uploaded Successfully"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new ApiResponse(new { url }, ApiStatusCodes.Success, false, "Uploaded Failed"));
        //    }
        //}
    }
}
