using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.Statistics.Api.Models;

namespace Hv.Sos100.DataService.Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStatisticsController : ControllerBase
    {
        private readonly EventStatisticsContext _context;

        public EventStatisticsController(EventStatisticsContext context)
        {
            _context = context;
        }

        // GET: api/EventStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventStatistics>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/EventStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventStatistics>> GetEventStatistics(int id)
        {
            var eventStatistics = await _context.Events.FindAsync(id);

            if (eventStatistics == null)
            {
                return NotFound();
            }

            return eventStatistics;
        }

        

        // POST: api/EventStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventStatistics>> PostEventStatistics(EventStatistics eventStatistics)
        {
            _context.Events.Add(eventStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventStatistics", new { id = eventStatistics.EventId }, eventStatistics);
        }

       
    }
}
