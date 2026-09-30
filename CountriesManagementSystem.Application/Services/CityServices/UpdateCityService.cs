using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Commands.Edit;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IUpdateCityService
    {
        Task<DomainResult> UpdateCity(long id, Command command, CancellationToken cancellationToken);
    }
    internal class UpdateCityService : BaseService, IUpdateCityService
    {
        private readonly ICityRepository _repository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCityService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICityRepository cityRepository
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _repository = cityRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DomainResult> UpdateCity(long id, Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            var country = await _countryRepository.FirstOrDefaultAsync(x => x.Id == command.CountryId);
            if (country is null)
                return DomainResult.Failure(["Country Not Found!"]);

            if (await _repository.IsNameExistedInCountry(command.Name, command.CountryId, id))
                return DomainResult.Failure(["This City Name Already Exist In This Country."]);

            var targetCity = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (targetCity is null)
                return DomainResult.Failure(["City Not Found!"]);

            targetCity.Update(command.Name, command.CountryId);

            _repository.Update(targetCity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
