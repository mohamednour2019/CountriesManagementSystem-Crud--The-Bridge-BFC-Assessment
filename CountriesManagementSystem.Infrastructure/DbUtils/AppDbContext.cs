using Microsoft.EntityFrameworkCore;
using CountriesManagementSystem.Domain.Entities;

namespace CountriesManagementSystem.Infrastructure.DbUtils
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<City> Cities => Set<City>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
