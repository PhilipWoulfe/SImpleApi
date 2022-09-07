using Microsoft.EntityFrameworkCore;
using SimpleApi.Core.ProjectAggregate;

namespace SimpleApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; } = null!;
    }
}
