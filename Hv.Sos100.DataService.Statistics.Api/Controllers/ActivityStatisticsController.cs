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
    public class ActivityStatisticsController : ControllerBase
    {
        private readonly EventStatisticsContext _context;

        public ActivityStatisticsController(EventStatisticsContext context)
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

        

        // POST: api/ActivityStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityStatistics>> PostActivityStatistics(ActivityStatistics activityStatistics)
        {
            _context.Activities.Add(activityStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivityStatistics", new { id = activityStatistics.ActivityId }, activityStatistics);
        }

        
    }
}
