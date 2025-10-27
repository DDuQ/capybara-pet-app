using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class Repository<T>(CapybaraPetAppDbContext dbContext) : IRepository<T>
    where T : Entity
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
}