using LogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LogApi.Data;

public class LogDbContext : DbContext
{
    public LogDbContext(DbContextOptions<LogDbContext> options) : base(options) {}

    public DbSet<Log> Logs { get; set; }
}