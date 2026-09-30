using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Infrastructure.DbUtils;


namespace CountriesManagementSystem.Infrastructure.Seed
{
    internal static class AppDbSeeder
    {
        public static async Task Seed(AppDbContext context)
        {
            if (!context.Set<Country>().Any())
            {
                var egypt = Country.Create("Egypt");
                var germany = Country.Create("Germany");
                var japan = Country.Create("Japan");

                var initialCountries = new List<Country> { egypt, germany, japan };
                await context.Set<Country>().AddRangeAsync(initialCountries);
                await context.SaveChangesAsync();

                var initialCities = new List<City>
                {
                    City.Create("Cairo", egypt.Id),
                    City.Create("Alexandria", egypt.Id),
                    City.Create("Berlin", germany.Id),
                    City.Create("Munich", germany.Id),
                    City.Create("Tokyo", japan.Id),
                    City.Create("Osaka", japan.Id)
                };
                await context.Set<City>().AddRangeAsync(initialCities);
                await context.SaveChangesAsync();
            }
        }
    }
}
