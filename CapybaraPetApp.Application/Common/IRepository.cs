namespace CapybaraPetApp.Application.Common;

public interface IRepository<T> where T : class
{
    Task Add(T entity);
    Task<T?> GetByIdAsync(Guid id);
    Task Update(T entity);
}
