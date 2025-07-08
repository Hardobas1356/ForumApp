using System.Linq.Expressions;

namespace ForumApp.Services.Core.Interfaces;

public interface IAsyncRepository<T>where T : class
{
    Task<T?> GetByIdAsync(Guid id,bool asNoTracking);
    Task<IEnumerable<T>> GetAllAsync(bool asNoTracking);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool asNoTracking);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> SaveChangesAsync();
}
