using Hv.Sos100.DataService.Statistics.AdminGui.Models;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Models
{
    public class StatisticsContext : DbContext
    {
        public DbSet<ActivityStatistics> Activity { get; set; }
        public DbSet<AdStatistics> Ad { get; set; }
        public DbSet<CountyStatistics> County { get; set; }
        public DbSet<EventStatistics> Event { get; set; }
    }
}
