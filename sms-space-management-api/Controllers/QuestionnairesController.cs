using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Entities.UserManagement;
using System.Security.Cryptography;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class QuestionnairesController : ControllerBase
    {
        private readonly IQuestionnairesService _service; 
        public QuestionnairesController(IQuestionnairesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetQuestionnairesMasterList")]
        public async Task<ActionResult<IEnumerable<Questionnaires>>> GetQuestionnairesMasterList()
        {
            var response = await _service.GetQuestionnairesMasterList();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet]
        [Route("GetQuestionnairesMasterById")]
        public async Task<ActionResult<Questionnaires>> GetQuestionnairesMasterById(int qId)
        {
            var response = await _service.GetQuestionnairesMasterById(qId);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpPost]
        [Route("CreateQuestionnaire")]
        public async Task<ActionResult<IEnumerable<Questionnaires>>> PostCreateQuestionnaire(QuestionnairePortal qpJson) 
        {
            var response = await _service.CreateQuestionnaire(qpJson);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpPost]
        [Route("AddQuestionnaire")]
        public async Task<ActionResult<Questionnaires>> PostAddQuestionnaire(Questionnaires addQuestionnaires)
        {
            var response = await _service.AddQuestionnaires(addQuestionnaires);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Added Successfully"));
        }

        [HttpGet("GetQuestionnaireById")]
        public async Task<ActionResult<QuestionnairePortal>> GetGetQuestionnaireById(int id)
        {
            var response = await _service.GetQuestionnaireById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }


        [HttpDelete("{qId}")]
        public async Task<IActionResult> DeleteQuestionnaire(int qId)
        {
            var response = await _service.DeleteQuestionnaire(qId);
            if (response == false)
            {
                return NotFound();
            }
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        }

        [HttpPost]
        [Route("SaveQuestionnaireAnswers")]
        public async Task<ActionResult<IEnumerable<Questionnaires>>> SaveQuestionnaireAnswers(QuestionnaireAnswer qaJson)
        {
            var response = await _service.SaveQuestionnaireAnswers(qaJson);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpGet("GetVisitorDetailsByDate")]
        public async Task<ActionResult<List<QuestionnaireAnswer>>> GetVisitorDetailsByDate(DateTime startDate, DateTime endDate)
        {
            var response = await _service.GetVisitorDetailsByDate(startDate, endDate);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }
    }
}
