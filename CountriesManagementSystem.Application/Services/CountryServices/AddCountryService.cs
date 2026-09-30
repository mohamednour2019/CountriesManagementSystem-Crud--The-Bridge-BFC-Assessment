using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Countries.Commands.Add;
using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CountryServices
{
    public interface IAddCountryService
    {
        Task<DomainResult> AddCountry(Command command, CancellationToken cancellationToken);
    }
    internal class AddCountryService : BaseService, IAddCountryService
    {
        private readonly ICountryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AddCountryService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _repository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DomainResult> AddCountry(Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            if (await _repository.IsNameExisted(command.Name))
                return DomainResult.Failure(["This Country Name Already Exist."]);


            var newCountry = Country.Create(command.Name);
            await _repository.AddAsync(newCountry);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
