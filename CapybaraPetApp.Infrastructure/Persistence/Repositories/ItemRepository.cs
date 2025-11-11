using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.ItemAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class ItemRepository(CapybaraPetAppDbContext dbContext) : Repository<Item>(dbContext), IItemRepository
{
    private readonly DbSet<Item> _items = dbContext.Item;

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _items.AnyAsync(item => item.Name == name);
    }

    public async Task<List<Item>> GetAllAsync(Guid userId)
    {
        return await _items.ToListAsync();
    }

    public async Task<List<Item>> GetByIdsAsync(List<Guid> ids)
    {
        return await _items.Where(i => ids.Contains(i.Id)).ToListAsync();
    }
}