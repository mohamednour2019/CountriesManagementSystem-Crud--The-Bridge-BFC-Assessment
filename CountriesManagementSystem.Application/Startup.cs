using FluentValidation;
using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.Services.CountryServices;
using CountriesManagementSystem.Application.Services.CityServices;
using Microsoft.Extensions.DependencyInjection;

namespace CountriesManagementSystem.Application
{
    public static class Startup
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(BaseService).Assembly);
            services.AddScoped<IAddCountryService, AddCountryService>();
            services.AddScoped<IUpdateCountryService, UpdateCountryService>();
            services.AddScoped<IDeleteCountryService, DeleteCountryService>();
            services.AddScoped<IGetCountryService, GetCountryService>();
            services.AddScoped<IGetCountriesListService, GetCountriesListService>();
            services.AddScoped<IAddCityService, AddCityService>();
            services.AddScoped<IUpdateCityService, UpdateCityService>();
            services.AddScoped<IDeleteCityService, DeleteCityService>();
            services.AddScoped<IGetCityService, GetCityService>();
            services.AddScoped<IGetCitiesListService, GetCitiesListService>();
            services.AddScoped<IGetCitiesByCountryIdService, GetCitiesByCountryIdService>();
        }

    }
}
