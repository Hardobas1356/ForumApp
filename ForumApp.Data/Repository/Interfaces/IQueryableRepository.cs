namespace ForumApp.Data.Repository.Interfaces;

public interface IQueryableRepository<T>
{
    IQueryable<T> GetQueryable(bool asNoTracking = true, bool ignoreQueryFilters = false);
}
