using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories._Bases;
using CountriesManagementSystem.Shared.SharedDTOs;

namespace CountriesManagementSystem.Domain.IRepositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<PageListResult<Country>> GetFilterdPagedListAsync(string? name, DateTime? createdFrom, DateTime? createdTo, int pageNumber, int pageSize);
        Task<bool> IsNameExisted(string name, long? exceptId = null);
        Task<bool> HasCities(long countryId);
    }
}
