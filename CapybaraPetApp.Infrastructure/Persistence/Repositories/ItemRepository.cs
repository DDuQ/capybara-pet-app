using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class ItemRepository : Repository<Item>, IItemRepository
{
    private readonly CapybaraPetAppDbContext _dbContext;

    public ItemRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserItem>> GetItemsByUserIdAsync(Guid userId)
    {
        var userItems = await _dbContext.UserItem
            .AsNoTracking()
            .Where(ui => ui.UserId == userId)
            .Include(ui => ui.Item)
            .ToListAsync();
        
        return userItems;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Set<Item>().AnyAsync(item => string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}