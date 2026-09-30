using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Countries.Commands.Delete;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CountryServices
{
    public interface IDeleteCountryService
    {
        Task<DomainResult> DeleteCountry(Command command, CancellationToken cancellationToken);
    }
    internal class DeleteCountryService : BaseService, IDeleteCountryService
    {
        private readonly ICountryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCountryService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _repository = countryRepository;
        }

        public async Task<DomainResult> DeleteCountry(Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            var targetCountry = await _repository.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (targetCountry is null)
                return DomainResult.Failure(["Country Not Found!"]);

            if (await _repository.HasCities(command.Id))
                return DomainResult.Failure(["Cannot delete country that has cities."]);

            _repository.Delete(targetCountry);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
