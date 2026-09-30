using Microsoft.EntityFrameworkCore;
using CountriesManagementSystem.Domain.Entities._Bases;
using CountriesManagementSystem.Domain.IRepositories._Bases;
using CountriesManagementSystem.Infrastructure.DbUtils;
using System.Linq.Expressions;


namespace CountriesManagementSystem.Infrastructure.Repositories._Bases
{
    internal class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : AppEntityBase
    {
        protected readonly DbSet<TEntity> _set;
        public RepositoryBase(AppDbContext appDbContext)
        {
            _set = appDbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _set.Remove(entity);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _set.FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity?> FirstOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _set.AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<TEntity?> FirstOrDefaultWithIncludesAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _set.AsNoTracking();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(filter);
        }

        public void Update(TEntity entity)
        {
            _set.Update(entity);
        }

    }
}
