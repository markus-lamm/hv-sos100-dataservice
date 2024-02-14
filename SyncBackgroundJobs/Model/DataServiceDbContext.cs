using Microsoft.EntityFrameworkCore;

namespace SyncBackgroundJobs.Model
{
    public class DataServiceDbContext : DbContext
    {
        public DataServiceDbContext(DbContextOptions<DataServiceDbContext> options) : base(options) { }
    }
}
