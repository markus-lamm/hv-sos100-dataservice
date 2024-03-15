﻿using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.Statistics.AdminGui.Models
{
    public class StatisticsContext :DbContext
    {
        public StatisticsContext(DbContextOptions<StatisticsContext> options) : base(options) { }

        public DbSet<EventStatistics> Events { get; set; }
        public DbSet<AdStatistics> Ads { get; set; }
        public DbSet<ActivityStatistics> Activities { get; set; }
    }
}
