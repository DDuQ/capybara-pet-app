using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class ItemRepository(CapybaraPetAppDbContext dbContext) : IRepository<Item>
{
    private readonly CapybaraPetAppDbContext _dbContext = dbContext;

    public async Task Add(Item entity)
    {
        await _dbContext.Item.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Item?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Item.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task Update(Item entity)
    {
        _dbContext.Item.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
