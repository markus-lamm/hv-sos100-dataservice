using Microsoft.EntityFrameworkCore;

namespace Hv.SOS100.DataService.Log.Api.Data;

public class LogDbContext : DbContext
{
    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) {}

    public DbSet<Models.Log> Logs { get; set; }
}