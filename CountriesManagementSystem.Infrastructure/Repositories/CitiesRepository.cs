using Microsoft.EntityFrameworkCore;
using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Infrastructure.DbUtils;
using CountriesManagementSystem.Infrastructure.Extensions;
using CountriesManagementSystem.Infrastructure.Repositories._Bases;
using CountriesManagementSystem.Shared.SharedDTOs;


namespace CountriesManagementSystem.Infrastructure.Repositories
{
    internal class CitiesRepository : RepositoryBase<City>, ICityRepository
    {
        public CitiesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        public async Task<PageListResult<City>> GetFilterdPagedListAsync(string? name, long? countryId, DateTime? createdFrom, DateTime? createdTo, int pageNumber, int pageSize)
        {
            var query = _set.AsNoTracking();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (countryId.HasValue)
                query = query.Where(p => p.CountryId == countryId.Value);

            if (createdFrom.HasValue)
                query = query.Where(p => p.CreatedAt >= createdFrom.Value);

            if (createdTo.HasValue)
                query = query.Where(p => p.CreatedAt <= createdTo.Value);

            return await query.OrderByDescending(p => p.CreatedAt).ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<PageListResult<City>> GetPagedListByCountryIdAsync(long countryId, int pageNumber, int pageSize)
        {
            var query = _set.AsNoTracking().Where(p => p.CountryId == countryId);
            return await query.OrderByDescending(p => p.CreatedAt).ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsNameExistedInCountry(string name, long countryId, long? exceptId = null)
        {
            if (exceptId.HasValue)
                return await _set.AnyAsync(p => p.Name == name && p.CountryId == countryId && p.Id != exceptId.Value);

            return await _set.AnyAsync(p => p.Name == name && p.CountryId == countryId);
        }
    }
}
