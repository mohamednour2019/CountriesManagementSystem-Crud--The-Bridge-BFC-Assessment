using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Countries.Queries.GetById;
using CountriesManagementSystem.Domain.IRepositories;


namespace CountriesManagementSystem.Application.Services.CountryServices
{
    public interface IGetCountryService
    {
        Task<DomainResult<Result>> GetCountry(Query query, CancellationToken cancellationToken = default);
    }
    internal class GetCountryService : BaseService, IGetCountryService
    {
        private readonly ICountryRepository _repository;
        public GetCountryService(IServiceProvider serviceProvider
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _repository = countryRepository;
        }

        public async Task<DomainResult<Result>> GetCountry(Query query, CancellationToken cancellationToken = default)
        {
            var validationDomainResult = await ValidateBeforeProcess(query);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return DomainResult.Failure<Result>(validationDomainResult.Messages.ToList());

            var country = await _repository.FirstOrDefaultWithIncludesAsync(x => x.Id == query.Id, c => c.Cities);

            if (country is null)
                return DomainResult.Failure<Result>(["Country Not Found!"]);

            Result result = new Result()
            {
                Id = country.Id,
                Name = country.Name,
                CreatedAt = country.CreatedAt,
                Cities = country.Cities?.Select(c => new CityDto { Id = c.Id, Name = c.Name }).ToList() ?? new List<CityDto>()
            };

            return DomainResult.Success(result);
        }


    }
}
