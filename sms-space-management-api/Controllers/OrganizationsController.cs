using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.application.Models.Dtos.Organization;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService _service;
        public OrganizationsController(IOrganizationService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetById(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpGet]
        [Route("getList")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateOrganizationDto request)
        {
            var response = await _service.Create(request);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Created Successfully"));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(UpdateOrganizationDto request)
        {
            if (request.OrgId != 0)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            if (response)
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, true, "Deleted Successfully"));
            else
                return NotFound();

        }

        //private readonly DataBaseContext _context;

        //public OrganizationsController(DataBaseContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Organizations
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Organization>>> GetOrganization()
        //{
        //    return await _context.Organization.ToListAsync();
        //}

        //// GET: api/Organizations/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Organization>> GetOrganization(int ID)
        //{
        //    var organization = await _context.Organization.FindAsync(ID);

        //    if (organization == null)
        //    {
        //        return NotFound();
        //    }

        //    return organization;
        //}

        //// PUT: api/Organizations/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrganization(int ID, Organization organization)
        //{
        //    if (ID != organization.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(organization).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrganizationExists(ID))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Organizations
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        //{
        //    _context.Organization.Add(organization);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (OrganizationExists(organization.ID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetOrganization", new { id = organization.ID }, organization);
        //}

        //// DELETE: api/Organizations/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrganization(int ID)
        //{
        //    var organization = await _context.Organization.FindAsync(ID);
        //    if (organization == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Organization.Remove(organization);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        ///// <summary>
        /////  to check organizaton eist or not
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <returns></returns>
        //private bool OrganizationExists(int ID)
        //{
        //    return _context.Organization.Any(e => e.ID == ID);
        //}
    }
}
