using System.Linq.Expressions;

namespace ForumApp.Services.Core.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, bool asNoTracking);
    Task<IEnumerable<T>> GetAllAsync(bool asNoTracking);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, bool asNoTracking);
    Task<T?> SingleOrDefaultWithIncludeAsync(Expression<Func<T, bool>> predicate,
                                             Func<IQueryable<T>, IQueryable<T>> include,
                                             bool asNoTracking);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
                                       bool asNoTracking);
    Task<IEnumerable<T>> GetWhereWithIncludeAsync(Expression<Func<T, bool>> predicate,
                                                  Func<IQueryable<T>, IQueryable<T>> include,
                                                  bool asNoTracking);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    Task<int> SaveChangesAsync();
}
