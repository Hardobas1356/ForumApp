using ForumApp.Data.Repository.Interfaces;

namespace ForumApp.Services.Core.Interfaces;

public interface IGenericRepository<T> : IAsyncRepository<T>, IIgnoreQueryFilters<T>,
    ISynchronousRepository<T>, IQueryableRepository<T>
    where T : class
{
}
