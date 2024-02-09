using Microsoft.EntityFrameworkCore;

namespace LogGui.Models
{
    public class LogContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public LogContext(DbContextOptions options) : base(options) { }
    }
}
