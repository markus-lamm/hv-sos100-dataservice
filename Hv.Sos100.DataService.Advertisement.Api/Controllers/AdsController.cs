using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hv.Sos100.DataService.Advertisement.Api.Model;
using Hv.Sos100.DataService.Adsvertisement.Data;

namespace Hv.Sos100.DataService.Advertisement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly AdsDbContext _context;

        public AdsController(AdsDbContext context)
        {
            _context = context;
        }

        [HttpGet("getallads")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetAllAds()
        {
            var ads = await _context.Ads.ToListAsync(); // Fetch all ads from the database

            if (ads == null)
            {
                return NotFound();
            }
            return ads;
        }

        [HttpGet("getallads/{TimeStamp}")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetAllAds(DateTime TimeStamp)
        {

            var ads = await _context.Ads
                .Where(ad => ad.TimeStamp > TimeStamp)
                .ToListAsync();
            if (ads == null)
            {
                return NotFound();
            }
            return ads;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ads>> GetAds(int id)
        {
            var ads = await _context.Ads.FindAsync(id);

            if (ads == null)
            {
                return NotFound();
            }

            return ads;
        }

        // GET: api/Ads
        [HttpGet("Vertical")]
        public async Task<ActionResult<Ads>> GetVerticalAd()
        {
            var ads = await _context.Ads.ToListAsync(); // Fetch all ads from the database

            if (ads == null || ads.Count == 0)
            {
                return NotFound();
            }

            var random = new Random();
            var randomObject = ads.Where(x => x.ImageDimension == "vertical").OrderBy(x => random.Next()).FirstOrDefault();

            if (randomObject == null)
            {
                return NotFound();
            }
            if (randomObject.TotalViews == null)
            {
                randomObject.TotalViews = 1;
            }
            else
            {
                randomObject.TotalViews += 1;
            }
            _context.Entry(randomObject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return randomObject;
        }

        // GET: api/Ads/5
        [HttpGet("Vertical/{number}")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetVerticalAds(int number)
        {
            var allAdvertisements = await _context.Ads.ToListAsync();

            var random = new Random();
            var shuffledAdvertisements = allAdvertisements.Where(x => x.ImageDimension == "vertical").OrderBy(x => random.Next()).ToList();

            var randomAdvertisements = shuffledAdvertisements.Take(number).ToList();

            if (randomAdvertisements == null)
            {
                return NotFound();
            }
            foreach (var ad in randomAdvertisements)
            {

                if (ad.TotalViews == null)
                {
                    ad.TotalViews = 1;
                }
                else
                {
                    ad.TotalViews += 1;
                }

                _context.Entry(ad).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return randomAdvertisements;
        }
        // GET: api/Ads
        [HttpGet("horizontal")]
        public async Task<ActionResult<Ads>> GetHorizontalAd()
        {
            var ads = await _context.Ads.ToListAsync(); // Fetch all ads from the database

            if (ads == null || ads.Count == 0)
            {
                return NotFound();
            }

            var random = new Random();
            var randomObject = ads.Where(x => x.ImageDimension == "horizontal").OrderBy(x => random.Next()).FirstOrDefault();

            if (randomObject == null)
            {
                return NotFound();
            }
            if (randomObject.TotalViews == null)
            {
                randomObject.TotalViews = 1;
            }
            else
            {
                randomObject.TotalViews += 1;
            }
            _context.Entry(randomObject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return randomObject;
        }

        // GET: api/Ads/5
        [HttpGet("horizontal/{number}")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetHorizontalAds(int number)
        {
            var allAdvertisements = await _context.Ads.ToListAsync();

            var random = new Random();
            var shuffledAdvertisements = allAdvertisements.Where(x => x.ImageDimension == "horizontal").OrderBy(x => random.Next()).ToList();

            var randomAdvertisements = shuffledAdvertisements.Take(number).ToList();

            if (randomAdvertisements == null)
            {
                return NotFound();
            }
            foreach (var ad in randomAdvertisements)
            {
                if (ad.TotalViews == null)
                {
                    ad.TotalViews = 1;
                }
                else
                {
                    ad.TotalViews += 1;
                }
                _context.Entry(ad).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return randomAdvertisements;
        }

        // GET: api/Ads
        [HttpGet("square")]
        public async Task<ActionResult<Ads>> GetsquareAd()
        {
            var ads = await _context.Ads.ToListAsync(); // Fetch all ads from the database

            if (ads == null || ads.Count == 0)
            {
                return NotFound();
            }

            var random = new Random();
            var randomObject = ads.Where(x => x.ImageDimension == "square").OrderBy(x => random.Next()).FirstOrDefault();

            if (randomObject == null)
            {
                return NotFound();
            }

            if (randomObject.TotalViews == null)
            {
                randomObject.TotalViews = 1;
            }
            else
            {
                randomObject.TotalViews += 1;
            }

            _context.Entry(randomObject).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return randomObject;
        }

        // GET: api/Ads/5
        [HttpGet("square/{number}")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetsquareAds(int number)
        {
            var allAdvertisements = await _context.Ads.ToListAsync();

            var random = new Random();
            var shuffledAdvertisements = allAdvertisements.Where(x => x.ImageDimension == "square").OrderBy(x => random.Next()).ToList();

            var randomAdvertisements = shuffledAdvertisements.Take(number).ToList();

            if (randomAdvertisements == null)
            {
                return NotFound();
            }
            foreach (var ad in randomAdvertisements)
            {
                if (ad.TotalViews == null)
                {
                    ad.TotalViews = 1;
                }
                else
                {
                    ad.TotalViews += 1;
                }
                _context.Entry(ad).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return randomAdvertisements;
        }

        // PUT: api/Ads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAds(int id, Ads ads)
        {
            if (id != ads.AdvertisementID)
            {
                return BadRequest();
            }

            _context.Entry(ads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdsExists(id))
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

        // POST: api/Ads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ads>> PostAds(Ads ads)
        {
            _context.Ads.Add(ads);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAds", new { id = ads.AdvertisementID }, ads);
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAds(int id)
        {
            var ads = await _context.Ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }

            _context.Ads.Remove(ads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdsExists(int id)
        {
            return _context.Ads.Any(e => e.AdvertisementID == id);
        }
    }
}
