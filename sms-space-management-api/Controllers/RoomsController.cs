﻿using System;
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
    public class RoomsController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public RoomsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoom()
        {
            return await _context.Room.ToListAsync();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.ID)
            {
                return BadRequest();
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _context.Room.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.ID }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Room.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.ID == id);
        }
    }
}
