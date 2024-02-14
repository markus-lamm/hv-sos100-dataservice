using Hv.Sos100.DataService.SingleSignOn.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Hv.Sos100.DataService.SingleSignOn.Api.Data;

public class AuthenticationDbContext : DbContext
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) {}

    public DbSet<User> Users { get; set; }
}