using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Queries.GetPagedList;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IGetCitiesListService
    {
        Task<DomainResult<PageListResult<Result>>> GetCitiesList(Query query, CancellationToken cancellationToken = default);
    }

    internal class GetCitiesListService : BaseService, IGetCitiesListService
    {
        private readonly ICityRepository _cityRepository;

        public GetCitiesListService(IServiceProvider serviceProvider, ICityRepository cityRepository) : base(serviceProvider)
        {
            _cityRepository = cityRepository;
        }

        public async Task<DomainResult<PageListResult<Result>>> GetCitiesList(Query query, CancellationToken cancellationToken)
        {
            var validation = await ValidateBeforeProcess(query);
            if (validation.IsFailure)
            {
                return DomainResult.Failure<PageListResult<Result>>(validation.Messages.ToList());
            }

            var pagedCities = await _cityRepository.GetFilterdPagedListAsync(
                query.Name,
                query.CountryId,
                query.CreatedFrom,
                query.CreatedTo,
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
