using Hv.Sos.DataService.Advertisement.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.Adsvertisement.Data;

public class AdsDbContext : DbContext
{
    public AdsDbContext(DbContextOptions<AdsDbContext> options) : base(options) {}

    public DbSet<Ads> Ads { get; set; }
}