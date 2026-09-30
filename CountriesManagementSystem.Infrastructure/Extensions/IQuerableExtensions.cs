using Microsoft.EntityFrameworkCore;
using CountriesManagementSystem.Shared.SharedDTOs;


namespace CountriesManagementSystem.Infrastructure.Extensions
{
    public static class IQuerableExtensions
    {
        public static async Task<PageListResult<T>> ToPagedResultAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {
            if (currentPage < 1) currentPage = 1;
            if (pageSize <= 0) pageSize = 10;

            long totalCount = await source.CountAsync();
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PageListResult<T>(items, totalCount);
        }
    }
}
