namespace ForumApp.Services.Core.Interfaces;

public interface IRepository<T> : IAsyncRepository<T> where T : class
{
}
