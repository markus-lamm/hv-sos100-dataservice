using Hv.Sos100.DataService.Advertisement.Api.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hv.Sos100.DataService.Adsvertisement.Data;

public class AdsDbContext : DbContext
{
    public AdsDbContext(DbContextOptions<AdsDbContext> options) : base(options) {}

    public DbSet<Ads> Ads { get; set; }

}