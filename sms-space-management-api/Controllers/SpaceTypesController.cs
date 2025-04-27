using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sms.space.management.data.access;
using sms.space.management.domain.Entities.Building;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class SpaceTypesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public SpaceTypesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/SpaceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpaceType>>> GetSpaceType()
        {
            return await _context.SpaceType.ToListAsync();
        }

        // GET: api/SpaceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceType>> GetSpaceType(int id)
        {
            var spaceType = await _context.SpaceType.FindAsync(id);

            if (spaceType == null)
            {
                return NotFound();
            }

            return spaceType;
        }

        // PUT: api/SpaceTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpaceType(int id, SpaceType spaceType)
        {
            if (id != spaceType.ID)
            {
                return BadRequest();
            }

            _context.Entry(spaceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpaceTypeExists(id))
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

        // POST: api/SpaceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SpaceType>> PostSpaceType(SpaceType spaceType)
        {
            _context.SpaceType.Add(spaceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpaceType", new { id = spaceType.ID }, spaceType);
        }

        // DELETE: api/SpaceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpaceType(int id)
        {
            var spaceType = await _context.SpaceType.FindAsync(id);
            if (spaceType == null)
            {
                return NotFound();
            }

            _context.SpaceType.Remove(spaceType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpaceTypeExists(int id)
        {
            return _context.SpaceType.Any(e => e.ID == id);
        }
    }
}
