namespace ForumApp.Services.Core.Interfaces;

public interface IGenericRepository<T> : IAsyncRepository<T>, ISynchronousRepository<T> where T : class
{
}
