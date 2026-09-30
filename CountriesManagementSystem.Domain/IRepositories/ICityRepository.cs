using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories._Bases;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Domain.IRepositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<PageListResult<City>> GetFilterdPagedListAsync(string? name, long? countryId, DateTime? createdFrom, DateTime? createdTo, int pageNumber, int pageSize);
        Task<PageListResult<City>> GetPagedListByCountryIdAsync(long countryId, int pageNumber, int pageSize);
        Task<bool> IsNameExistedInCountry(string name, long countryId, long? exceptId = null);
    }
}
