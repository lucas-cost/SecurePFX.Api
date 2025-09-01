using Microsoft.EntityFrameworkCore;
using SecurePFX.Domain.Entities;

namespace SecurePFX.Infrastructure.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
