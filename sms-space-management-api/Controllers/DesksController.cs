using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;

namespace sms.space.management.api.Controllers
{

    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class DesksController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly IDeskService _deskService;

        public DesksController(DataBaseContext context, IDeskService deskService)
        {
            _context = context;
            _deskService = deskService;
        }

        // GET: api/Desks
        [HttpPost("GetDesk")]
        public async Task<IActionResult> GetDesk(SpaceFilter filter)
        {
            if (filter == null)
            {
                return Ok(new ApiResponse(null, ApiStatusCodes.InputValidationFailure, false, "Input Error"));
            }
            else
            {
                var response = await _deskService.GetAvailableDeskList(filter);
                return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
            }
        }

        // GET: api/Desks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desk>> GetDesk(int id)
        {
            var desk = await _context.Desk.FindAsync(id);

            if (desk == null)
            {
                return NotFound();
            }

            return desk;
        }

        // PUT: api/Desks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDesk(int id, Desk desk)
        {
            if (id != desk.Id)
            {
                return BadRequest();
            }

            _context.Entry(desk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Desks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Desk>> PostDesk(Desk desk)
        {
            _context.Desk.Add(desk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDesk", new { id = desk.Id }, desk);
        }

        // DELETE: api/Desks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesk(int id)
        {
            var desk = await _context.Desk.FindAsync(id);
            if (desk == null)
            {
                return NotFound();
            }

            _context.Desk.Remove(desk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeskExists(int id)
        {
            return _context.Desk.Any(e => e.Id == id);
        }
    }
}
