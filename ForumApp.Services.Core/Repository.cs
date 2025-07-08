using ForumApp.Data;
using ForumApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace ForumApp.Services.Core;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ForumAppDbContext dbContext;
    private readonly DbSet<T> dbSet;

    public Repository(ForumAppDbContext dbContext)
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

    public Task DeleteAsync(T entity)
    {
        dbSet
            .Remove(entity);
        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        dbSet
            .RemoveRange(entities);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbSet
             .AnyAsync(predicate);
    }
    public async Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = false)
    {
        IQueryable<T> query = dbSet;
        if (asNoTracking)
            query = query.AsNoTracking();

        return await query
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id, bool asNoTracking = false)
    {
        IQueryable<T> query = dbSet;

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query
            .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }
    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false)
    {
        IQueryable<T> query = dbSet
            .Where(predicate);

        if (asNoTracking)
        {
            query = query
                .AsNoTracking();
        }

        return await query
            .ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await Task.CompletedTask;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

}
