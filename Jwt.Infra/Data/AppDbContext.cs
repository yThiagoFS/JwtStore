using Jwt.Core.Contexts.AccountContext.Entities;
using Jwt.Infra.Contexts.AccountContexts.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts)
        {}

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
        }

    }
}
