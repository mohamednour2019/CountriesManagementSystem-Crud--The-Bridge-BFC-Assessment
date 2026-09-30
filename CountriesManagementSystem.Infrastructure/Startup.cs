using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;
using CountriesManagementSystem.Infrastructure.DbUtils;
using CountriesManagementSystem.Infrastructure.Repositories;
using CountriesManagementSystem.Infrastructure.Seed;


namespace CountriesManagementSystem.Infrastructure
{
    public static class Startup
    {
        public static void AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("default")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICountryRepository, CountriesRepository>();
            services.AddScoped<ICityRepository, CitiesRepository>();
        }

        public static async Task ApplyDatabaseMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync();
                    await AppDbSeeder.Seed(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<AppDbContext>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }

    }
}
