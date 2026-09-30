using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Queries.GetById;
using CountriesManagementSystem.Domain.IRepositories;


namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IGetCityService
    {
        Task<DomainResult<Result>> GetCity(Query query, CancellationToken cancellationToken = default);
    }
    internal class GetCityService : BaseService, IGetCityService
    {
        private readonly ICityRepository _repository;
        public GetCityService(IServiceProvider serviceProvider
            , ICityRepository cityRepository) : base(serviceProvider)
        {
            _repository = cityRepository;
        }

        public async Task<DomainResult<Result>> GetCity(Query query, CancellationToken cancellationToken = default)
        {
            var validationDomainResult = await ValidateBeforeProcess(query);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return DomainResult.Failure<Result>(validationDomainResult.Messages.ToList());

            var city = await _repository.FirstOrDefaultWithIncludesAsync(x => x.Id == query.Id, c => c.Country);

            if (city is null)
                return DomainResult.Failure<Result>(["City Not Found!"]);

            Result result = new Result()
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                CountryName = city.Country?.Name,
                CreatedAt = city.CreatedAt,
            };

            return DomainResult.Success(result);
        }


    }
}
