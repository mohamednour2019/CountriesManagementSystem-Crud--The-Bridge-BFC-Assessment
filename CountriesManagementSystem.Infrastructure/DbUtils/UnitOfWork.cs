using Microsoft.EntityFrameworkCore.Storage;
using CountriesManagementSystem.Domain.Shared;


namespace CountriesManagementSystem.Infrastructure.DbUtils
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction currentTransaction;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task BeginTransaction()
        {
            currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaciton()
        {
            await currentTransaction.CommitAsync();
        }

        public async Task RollbackTransaction()
        {
            await currentTransaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
