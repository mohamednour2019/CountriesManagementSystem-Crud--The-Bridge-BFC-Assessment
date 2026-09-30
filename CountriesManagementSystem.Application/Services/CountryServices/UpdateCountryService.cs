using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Countries.Commands.Edit;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CountryServices
{
    public interface IUpdateCountryService
    {
        Task<DomainResult> UpdateCountry(long id, Command command, CancellationToken cancellationToken);
    }
    internal class UpdateCountryService : BaseService, IUpdateCountryService
    {
        private readonly ICountryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCountryService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICountryRepository countryRepository) : base(serviceProvider)
        {
            _repository = countryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DomainResult> UpdateCountry(long id, Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            if (await _repository.IsNameExisted(command.Name, id))
                return DomainResult.Failure(["This Country Name Already Exist."]);

            var targetCountry = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (targetCountry is null)
                return DomainResult.Failure(["Country Not Found!"]);

            targetCountry.Update(command.Name);

            _repository.Update(targetCountry);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
