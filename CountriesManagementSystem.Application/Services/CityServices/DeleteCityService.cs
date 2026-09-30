using CountriesManagementSystem.Application.Common;
using CountriesManagementSystem.Application.DTOs.Cities.Commands.Delete;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Application.Services.CityServices
{
    public interface IDeleteCityService
    {
        Task<DomainResult> DeleteCity(Command command, CancellationToken cancellationToken);
    }
    internal class DeleteCityService : BaseService, IDeleteCityService
    {
        private readonly ICityRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCityService(IServiceProvider serviceProvider
            , IUnitOfWork unitOfWork
            , ICityRepository cityRepository) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _repository = cityRepository;
        }

        public async Task<DomainResult> DeleteCity(Command command, CancellationToken cancellationToken)
        {
            var validationDomainResult = await ValidateBeforeProcess(command);

            if (validationDomainResult != null && validationDomainResult.IsFailure)
                return validationDomainResult;

            var targetCity = await _repository.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (targetCity is null)
                return DomainResult.Failure(["City Not Found!"]);

            _repository.Delete(targetCity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return DomainResult.Success();
        }
    }
}
