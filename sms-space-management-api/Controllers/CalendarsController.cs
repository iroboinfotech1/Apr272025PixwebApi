using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.domain.Entities.Common;
using sms.space.management.data.access;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase 
    {
        private readonly DataBaseContext _context;

        public CalendarsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendar()
        {
            return await _context.Calendar.ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(int id)
        {
            var calendar = await _context.Calendar.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(int id, Calendar calendar)
        {
            if (id != calendar.ID)
            {
                return BadRequest();
            }

            _context.Entry(calendar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarExists(id))
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

        // POST: api/Calendars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(Calendar calendar)
        {
            _context.Calendar.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendar", new { id = calendar.ID }, calendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }

            _context.Calendar.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarExists(int id)
        {
            return _context.Calendar.Any(e => e.ID == id);
        }
    }
}
