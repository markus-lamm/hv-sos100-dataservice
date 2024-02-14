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
    public class AdStatisticsController : ControllerBase
    {
        private readonly StatisticsContext _context;

        public AdStatisticsController(StatisticsContext context)
        {
            _context = context;
        }

        // GET: api/AdStatistics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdStatistics>>> GetAds()
        {
            return await _context.Ads.ToListAsync();
        }

        // GET: api/AdStatistics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdStatistics>> GetAdStatistics(int id)
        {
            var adStatistics = await _context.Ads.FindAsync(id);

            if (adStatistics == null)
            {
                return NotFound();
            }

            return adStatistics;
        }

        

        // POST: api/AdStatistics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdStatistics>> PostAdStatistics(AdStatistics adStatistics)
        {
            _context.Ads.Add(adStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdStatistics", new { id = adStatistics.AdId }, adStatistics);
        }

        
    }
}
