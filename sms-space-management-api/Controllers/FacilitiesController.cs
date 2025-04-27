using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.api.Controllers;

[Route("api/SMSService/[controller]")]
[ApiController]
public class FacilitiesController : ControllerBase
{
    private readonly IFacilitiesService _service;

    public FacilitiesController(IFacilitiesService service)
    {
        this._service = service;
    }

    // GET: api/Facilities

    [HttpGet]
    [Route("GetAllFacilityTypes")]
    public async Task<ActionResult<IEnumerable<FacilityTypes>>> GetAllFacilityTypes()
    {
        var response = await _service.GetAllFacilityTypes();
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }

    [HttpGet]
    [Route("getByOrganization/{orgId}")]
    public async Task<ActionResult<IEnumerable<Facilities>>> GetFacilities(int orgId)
    {
        var response = await _service.GetByOrganization(orgId);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }

    [HttpGet]
    [Route("GetByOrganizationAndFacilityType/{orgId}/{facilityTypeId}")]
    public async Task<ActionResult<IEnumerable<Facilities>>> GetFacilities(int orgId,int facilityTypeId)
    {
        var response = await _service.GetByOrganizationAndFacilityType(orgId, facilityTypeId);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }

    [HttpGet]
    [Route("getAll")]
    public async Task<ActionResult<IEnumerable<Facilities>>> GetAll()
    {
        var response = await _service.GetAll();
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }

    // GET: api/Facilities/5

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<Facilities>>> Get(int id)
    {
        var response = await _service.GetById(id);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }

    [HttpPut]
    public async Task<IActionResult> PutFacilities(UpdateFacilitiesDto facilities)
    {
        if (facilities != null && facilities.FacilityId != 0)
        {
            var response = await _service.Update(facilities.FacilityId, facilities);

            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
            else
                return NotFound();
        }
        else
            return BadRequest();
    }


    [HttpPost]
    public async Task<ActionResult<GetFacilitiesDto>> PostFacilities(CreateFacilitiesDto facilities)
    {
        var response = await _service.Create(facilities);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
    }

    // DELETE: api/Facilities/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFacilities(int ID)
    {
        var response = await _service.Delete(ID);
        if (response == false)
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        else
            return NotFound();

    }

    [HttpGet]
    [Route("GetFacilitiesById/{facilityId}")]
    public async Task<ActionResult<Facilities>> GetFacilitiesById(int facilityId)
    {
        var response = await _service.GetFacilitiesById(facilityId);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }
}


[Route("api/SMSService/[controller]")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IResourcesService _service;

    public ResourcesController(IResourcesService service)
    {
        this._service = service;
    }



    [HttpGet]
    [Route("getByFacility/{facilityId}")]
    public async Task<ActionResult<IEnumerable<Facilities>>> GetFacilities(int facilityId)
    {
        var response = await _service.GetByFacility(facilityId);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
    }


    [HttpPut]
    public async Task<IActionResult> Put(UpdateResourcesDto request)
    {
        if (request != null && request.FacilityId != 0)
        {
            var response = await _service.Update(request);

            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
            else
                return NotFound();
        }
        else
            return BadRequest();
    }


    [HttpPost]
    public async Task<ActionResult<GetResourcesDto>> Post(CreateResourcesDto request)
    {
        var response = await _service.Create(request);
        return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.Delete(id);
        if (response == false)
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
        else
            return NotFound();

    }
    [HttpGet("GetResourceList/{floorId}")]
    public async Task<ActionResult<IEnumerable<Resource>>> GetResourceList(int floorId)
    {
        var response = await _service.GetResourceList(floorId);

        return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Record Found"));

    }

    [HttpPut("UpdateResourceStatus")]
    public async Task<IActionResult> UpdateResourceStatus(UpdateResourcesDto request)
    {
        if (request != null && request.FacilityId != 0)
        {
            var response = await _service.UpdateResourceStatus(request);

            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Updated Successfully"));
            else
                return NotFound();
        }
        else
            return BadRequest();
    }
}