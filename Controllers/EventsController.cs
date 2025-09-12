using EventXFullApi.Data;
using EventXFullApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventXFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Events.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = ev.Id }, ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Event ev)
        {
            var dbEvent = await _context.Events.FindAsync(id);
            if (dbEvent == null) return NotFound();

            dbEvent.Title = ev.Title;
            dbEvent.Description = ev.Description;
            dbEvent.Date = ev.Date;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
