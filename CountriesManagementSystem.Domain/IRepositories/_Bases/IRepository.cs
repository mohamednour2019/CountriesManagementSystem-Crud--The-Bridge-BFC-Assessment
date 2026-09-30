using CountriesManagementSystem.Domain.Entities._Bases;
using System.Linq.Expressions;

namespace CountriesManagementSystem.Domain.IRepositories._Bases
{
    public interface IRepository<TEntity>
        where TEntity : AppEntityBase
    {
        #region Add
        public Task AddAsync(TEntity entity);
        #endregion

        #region Get
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);
        public Task<TEntity?> FirstOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter);
        public Task<TEntity?> FirstOrDefaultWithIncludesAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        #endregion

        #region Update

        public void Update(TEntity entity);
        #endregion

        #region Delete
        public void Delete(TEntity entity);
        #endregion
    }
}
