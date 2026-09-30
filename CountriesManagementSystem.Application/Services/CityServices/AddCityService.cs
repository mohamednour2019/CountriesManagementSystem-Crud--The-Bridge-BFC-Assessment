using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Commands.Add;
using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IAddCityService
    {
        Task<DomainResult> AddCity(Command command, CancellationToken cancellationToken);
    }
    internal class AddCityService : BaseService, IAddCityService
    {
        private readonly ICityRepository _repository;
        private readonly ICountryRepository _countryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddCityService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICityRepository cityRepository
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _repository = cityRepository;
            _countryRepository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DomainResult> AddCity(Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            var country = await _countryRepository.FirstOrDefaultAsNoTrackingAsync(x => x.Id == command.CountryId);
            if (country is null)
                return DomainResult.Failure(["Country Not Found!"]);

            if (await _repository.IsNameExistedInCountry(command.Name, command.CountryId))
                return DomainResult.Failure(["This City Name Already Exist In This Country."]);


            var newCity = City.Create(command.Name, command.CountryId);
            await _repository.AddAsync(newCity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
