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
    public class CountyStatisticsController : ControllerBase
    {
        private readonly StatisticsContext _context;

        public CountyStatisticsController(StatisticsContext context)
        {
            _context = context;
        }

        // GET: api/CountyStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountyStatistics>>> GetCounties()
        {
            return await _context.Counties.ToListAsync();
        }

        // GET: api/CountyStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountyStatistics>> GetCountyStatistics(int id)
        {
            var countyStatistics = await _context.Counties.FindAsync(id);

            if (countyStatistics == null)
            {
                return NotFound();
            }

            return countyStatistics;
        }

        // PUT: api/CountyStatistics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountyStatistics(int id, CountyStatistics countyStatistics)
        {
            if (id != countyStatistics.Id)
            {
                return BadRequest();
            }

            _context.Entry(countyStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyStatisticsExists(id))
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

        // POST: api/CountyStatistics
        [HttpPost]
        public async Task<ActionResult<CountyStatistics>> PostCountyStatistics(CountyStatistics countyStatistics)
        {
            _context.Counties.Add(countyStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountyStatistics", new { id = countyStatistics.Id }, countyStatistics);
        }

        // DELETE: api/CountyStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountyStatistics(int id)
        {
            var countyStatistics = await _context.Counties.FindAsync(id);
            if (countyStatistics == null)
            {
                return NotFound();
            }

            _context.Counties.Remove(countyStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountyStatisticsExists(int id)
        {
            return _context.Counties.Any(e => e.Id == id);
        }
    }
}
