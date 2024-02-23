﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.Statistics.Api.Models;
using Hv.Sos100.DataService.Statistics.Api.Data;

namespace Hv.Sos100.DataService.Statistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventStatisticsController : ControllerBase
    {
        private readonly StatisticsContext _context;

        public EventStatisticsController(StatisticsContext context)
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

        // PUT: api/EventStatistics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventStatistics(int id, EventStatistics eventStatistics)
        {
            if (id != eventStatistics.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventStatisticsExists(id))
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

        // POST: api/EventStatistics
        [HttpPost]
        public async Task<ActionResult<EventStatistics>> PostEventStatistics(EventStatistics eventStatistics)
        {
            _context.Events.Add(eventStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventStatistics", new { id = eventStatistics.Id }, eventStatistics);
        }

        // DELETE: api/EventStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventStatistics(int id)
        {
            var eventStatistics = await _context.Events.FindAsync(id);
            if (eventStatistics == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventStatisticsExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
