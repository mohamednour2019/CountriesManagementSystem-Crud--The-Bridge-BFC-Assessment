using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Countries.Queries.GetPagedList;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Application.Services.CountryServices
{
    public interface IGetCountriesListService
    {
        Task<DomainResult<PageListResult<Result>>> GetCountriesList(Query query, CancellationToken cancellationToken = default);
    }

    internal class GetCountriesListService : BaseService, IGetCountriesListService
    {
        private readonly ICountryRepository _countryRepository;

        public GetCountriesListService(IServiceProvider serviceProvider, ICountryRepository countryRepository) : base(serviceProvider)
        {
            _countryRepository = countryRepository;
        }

        public async Task<DomainResult<PageListResult<Result>>> GetCountriesList(Query query, CancellationToken cancellationToken)
        {
            var validation = await ValidateBeforeProcess(query);
            if (validation.IsFailure)
            {
                return DomainResult.Failure<PageListResult<Result>>(validation.Messages.ToList());
            }

            var pagedCountries = await _countryRepository.GetFilterdPagedListAsync(
                query.Name,
                query.CreatedFrom,
                query.CreatedTo,
                query.Paginator.CurrentPage,
                query.Paginator.PageSize);

            var countryDtos = pagedCountries.DataList.Select(x => new Result
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt
            }).ToList();

            var finalResult = new PageListResult<Result>(countryDtos, pagedCountries.TotalCount);

            return DomainResult.Success(finalResult);
        }
    }
}
