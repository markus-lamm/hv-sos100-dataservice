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

        

        // POST: api/CountyStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CountyStatistics>> PostCountyStatistics(CountyStatistics countyStatistics)
        {
            _context.Counties.Add(countyStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountyStatistics", new { id = countyStatistics.CountyId }, countyStatistics);
        }

        
    }
}
