using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.domain.Entities.Organization;

namespace sms.space.management.api.Controllers
{

    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        //private readonly DataBaseContext _context;
        private readonly IStateService _service;

        public StatesController(IStateService service)//DataBaseContext context,
        {
            // _context = context;
            _service = service;
        }

        // GET: api/States
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<State>>> GetState()
        //{
        //    return await _context.State.ToListAsync();
        //}

        //// GET: api/States/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<State>> GetState(int ID)
        //{
        //    var state = await _context.State.FindAsync(ID);

        //    if (state == null)
        //    {
        //        return NotFound();
        //    }

        //    return state;
        //}

        [HttpGet]
        [Route("getByCountry/{id}")]
        public async Task<ActionResult<State>> GetByCountryId(int id)
        {
            var response = await _service.GetByCountryId(id);
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutState(int ID, State state)
        //{
        //    if (ID != state.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(state).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StateExists(ID))
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

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<State>> PostState(State state)
        //{
        //    _context.State.Add(state);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StateExists(state.ID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetState", new { id = state.ID }, state);
        //}

        //// DELETE: api/States/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteState(int ID)
        //{
        //    var state = await _context.State.FindAsync(ID);
        //    if (state == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.State.Remove(state);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool StateExists(int ID)
        //{
        //    return _context.State.Any(e => e.ID == ID);
        //}
    }
}
