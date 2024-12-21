using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace CharityHub.DomainService.Abstractions.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RolleBackAsync();
    }
}
