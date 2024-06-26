﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.Statistics.Api.Models;
using Hv.Sos100.DataService.Statistics.Api.Data;

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

        // PUT: api/AdStatistics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdStatistics(int id, AdStatistics adStatistics)
        {
            if (id != adStatistics.AdvertisementStatisticsID)
            {
                return BadRequest();
            }

            _context.Entry(adStatistics).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdStatisticsExists(id))
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

        // POST: api/AdStatistics
        [HttpPost]
        public async Task<ActionResult<AdStatistics>> PostAdStatistics(AdStatistics adStatistics)
        {
            _context.Ads.Add(adStatistics);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdStatistics", new { id = adStatistics.AdvertisementStatisticsID }, adStatistics);
        }

        // POST: api/AdStatistics/list
        [HttpPost ("ad/list")]
        public async Task<ActionResult> PostAdStatisticsList(List<AdStatistics> adStatisticsList)
        {
            foreach (AdStatistics adStatistics in adStatisticsList)
            {
                var existingAd = await _context.Ads.FirstOrDefaultAsync(a => a.AdvertisementID == adStatistics.AdvertisementID);

                if (existingAd == null)
                {
                    // No existing AdStatistics found with the same advertisementID, so add it to the database
                    _context.Ads.Add(adStatistics);
                }
                else
                {
                    // Existing AdStatistics found with the same advertisementID, so update it
                    adStatistics.AdvertisementStatisticsID = existingAd.AdvertisementStatisticsID;
                    _context.Entry(existingAd).CurrentValues.SetValues(adStatistics);
                }
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/AdStatistics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdStatistics(int id)
        {
            var adStatistics = await _context.Ads.FindAsync(id);
            if (adStatistics == null)
            {
                return NotFound();
            }

            _context.Ads.Remove(adStatistics);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdStatisticsExists(int id)
        {
            return _context.Ads.Any(e => e.AdvertisementStatisticsID == id);
        }
    }
}
