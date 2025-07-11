using ForumApp.Data;
using ForumApp.Data.Repository.Interfaces;
using ForumApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ForumApp.Services.Core;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ForumAppDbContext dbContext;
    private readonly DbSet<T> dbSet;

    public GenericRepository(ForumAppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet
            .AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await dbSet
            .AddRangeAsync(entities);
    }

    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        dbSet
            .RemoveRange(entities);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,
                                     bool asNoTracking,
                                     bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        return await query
             .AnyAsync(predicate);
    }
    public async Task<IEnumerable<T>> GetAllWithInludeAsync(Func<IQueryable<T>, IQueryable<T>> include,
                                                      bool asNoTracking = true,
                                                      bool ignoreQueryFilters = false)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        query = include(query);

        return await query
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking,
                                                  bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        return await query
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id,
                                       bool asNoTracking,
                                       bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        return await query
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate,
                                                    bool asNoTracking,
                                                    bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        return await query
            .ToListAsync();
    }
    public async Task<IEnumerable<T>> GetWhereWithIncludeAsync(Expression<Func<T, bool>> predicate,
                                                               Func<IQueryable<T>, IQueryable<T>> include,
                                                               bool asNoTracking,
                                                               bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters)
            .Where(predicate);
        query = include(query);
        return await query.ToListAsync();
    }
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,
                                                bool asNoTracking,
                                                bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters);

        return await query
            .SingleOrDefaultAsync(predicate);
    }

    public async Task<T?> SingleOrDefaultWithIncludeAsync(Expression<Func<T, bool>> predicate,
                                                    Func<IQueryable<T>, IQueryable<T>> include,
                                                    bool asNoTracking,
                                                    bool ignoreQueryFilters)
    {
        IQueryable<T> query = BuildQueryable(asNoTracking, ignoreQueryFilters)
            .Where(predicate);

        query = include(query);

        return await query
           .SingleOrDefaultAsync();
    }

    public void Update(T entity)
    {
        dbSet.Update(entity);
    }
    public IQueryable<T> IgnoreQueryFilters()
    {
        return dbSet.IgnoreQueryFilters();
    }

    private IQueryable<T> BuildQueryable(bool asNoTracking,
                                         bool ignoreQueryFilters)
    {
        IQueryable<T> query = dbSet;

        if (ignoreQueryFilters)
            query = query.IgnoreQueryFilters();

        if (asNoTracking)
            query = query.AsNoTracking();

        return query;
    }
}
