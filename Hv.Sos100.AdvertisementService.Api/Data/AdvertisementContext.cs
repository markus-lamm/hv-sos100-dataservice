using Hv.Sos100.AdvertisementService.Api.Models;
using Microsoft.EntityFrameworkCore;



namespace Hv.Sos100.AdvertisementService.Api.Data
{
    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options) : base(options) { }
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}
