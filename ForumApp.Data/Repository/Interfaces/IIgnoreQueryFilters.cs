namespace ForumApp.Data.Repository.Interfaces;

public interface IIgnoreQueryFilters<T> where T : class
{
    IQueryable<T> IgnoreQueryFilters();
}
