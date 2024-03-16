using System;
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
    public class ActivityStatisticsController : ControllerBase
    {
        private readonly StatisticsContext _context;

        public ActivityStatisticsController(StatisticsContext context)
        {
            _context = context;
        }

        // GET: api/ActivityStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityStatistics>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        // GET: api/ActivityStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityStatistics>> GetActivityStatistics(int id)
        {
            var activityStatistics = await _context.Activities.FindAsync(id);

            if (activityStatistics == null)
            {
                return NotFound();
            }

            return activityStatistics;
        }

        // PUT: api/ActivityStatistics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityStatistics(int id, ActivityStatistics activityStatistics)
        {
            if (id != activityStatistics.ActivityStatisticsID)
            {
                return BadRequest();
            }

            _context.Entry(activityStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityStatisticsExists(id))
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

        // POST: api/ActivityStatistics
        [HttpPost]
        public async Task<ActionResult<ActivityStatistics>> PostActivityStatistics(ActivityStatistics activityStatistics)
        {
            _context.Activities.Add(activityStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityStatistics", new { id = activityStatistics.ActivityStatisticsID }, activityStatistics);
        }

        // POST: api/ActivityStatistics/list
        [HttpPost("activity/list")]
        public async Task<ActionResult> PostActivityStatisticsList(List<ActivityStatistics> activityStatisticsList)
        {
            foreach(ActivityStatistics activityStatistics in activityStatisticsList)
            {
                _context.Activities.Add(activityStatistics);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ActivityStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityStatistics(int id)
        {
            var activityStatistics = await _context.Activities.FindAsync(id);
            if (activityStatistics == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activityStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityStatisticsExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityStatisticsID == id);
        }
    }
}
