using Microsoft.EntityFrameworkCore;
using CountriesManagementSystem.Domain.Entities;
using CountriesManagementSystem.Domain.IRepositories;
using CountriesManagementSystem.Infrastructure.DbUtils;
using CountriesManagementSystem.Infrastructure.Extensions;
using CountriesManagementSystem.Infrastructure.Repositories._Bases;
using CountriesManagementSystem.Shared.SharedDTOs;


namespace CountriesManagementSystem.Infrastructure.Repositories
{
    internal class CountriesRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountriesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }


        public async Task<PageListResult<Country>> GetFilterdPagedListAsync(string? name, DateTime? createdFrom, DateTime? createdTo, int pageNumber, int pageSize)
        {
            var query = _set.AsNoTracking();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (createdFrom.HasValue)
                query = query.Where(p => p.CreatedAt >= createdFrom.Value);

            if (createdTo.HasValue)
                query = query.Where(p => p.CreatedAt <= createdTo.Value);

            return await query.OrderByDescending(p => p.CreatedAt).ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsNameExisted(string name, long? exceptId = null)
        {
            if (exceptId.HasValue)
                return await _set.AnyAsync(p => p.Name == name && p.Id != exceptId.Value);

            return await _set.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> HasCities(long countryId)
            => await _set.Where(c => c.Id == countryId).SelectMany(c => c.Cities).AnyAsync();
    }
}
