﻿namespace ForumApp.Services.Core.Interfaces;

public interface ISynchronousRepository<T> where T : class
{
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
}
