namespace CountriesManagementSystem.Domain.Shared
{
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        Task CommitTransaciton();
        Task RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
