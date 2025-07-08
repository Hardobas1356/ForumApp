namespace ForumApp.Services.Core.Interfaces;

public interface IGenericRepository<T> : IAsyncRepository<T> where T : class
{
}
