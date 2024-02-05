using log_api.Models;
using Microsoft.EntityFrameworkCore;

namespace log_api.Data;

public class LogDbContext : DbContext
{
    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) {}

    public DbSet<Log> Logs { get; set; }
}