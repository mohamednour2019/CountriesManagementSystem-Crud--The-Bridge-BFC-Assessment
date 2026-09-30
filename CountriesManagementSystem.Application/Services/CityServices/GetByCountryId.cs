using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Queries.GetByCountryId;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IGetCitiesByCountryIdService
    {
        Task<DomainResult<PageListResult<Result>>> GetCitiesByCountryId(long countryId, Query query, CancellationToken cancellationToken = default);
    }

    internal class GetCitiesByCountryIdService : BaseService, IGetCitiesByCountryIdService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public GetCitiesByCountryIdService(
            IServiceProvider serviceProvider,
            ICityRepository cityRepository,
            ICountryRepository countryRepository) : base(serviceProvider)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<DomainResult<PageListResult<Result>>> GetCitiesByCountryId(long countryId, Query query, CancellationToken cancellationToken)
        {
            var validation = await ValidateBeforeProcess(query);
            if (validation.IsFailure)
            {
                return DomainResult.Failure<PageListResult<Result>>(validation.Messages.ToList());
            }

            var country = await _countryRepository.FirstOrDefaultAsNoTrackingAsync(x => x.Id == countryId);
            if (country is null)
                return DomainResult.Failure<PageListResult<Result>>(["Country Not Found!"]);

            var pagedCities = await _cityRepository.GetPagedListByCountryIdAsync(
                countryId,
                query.Paginator.CurrentPage,
                query.Paginator.PageSize);

            var cityDtos = pagedCities.DataList.Select(x => new Result
            {
                Id = x.Id,
                Name = x.Name,
                CountryId = x.CountryId,
                CreatedAt = x.CreatedAt
            }).ToList();

            var finalResult = new PageListResult<Result>(cityDtos, pagedCities.TotalCount);

            return DomainResult.Success(finalResult);
        }
    }
}
