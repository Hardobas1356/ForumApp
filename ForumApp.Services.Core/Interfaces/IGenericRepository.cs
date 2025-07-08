namespace ForumApp.Services.Core.Interfaces;

public interface IGenericRepository<T> : IAsyncRepository<T>, IRepository<T> where T : class
{
}
