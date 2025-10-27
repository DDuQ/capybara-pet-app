namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IRepository<T>
{
    Task AddAsync(T entity);

    Task<T?> GetByIdAsync(Guid id);

    void UpdateAsync(T entity);
}