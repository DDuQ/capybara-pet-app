using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Application.Common;

public interface IItemRepository
{
    Task AddAsync(Item item);

    Task<Item?> GetByIdAsync(Guid id);
    
    Task<List<UserItem>> GetItemsByUserIdAsync(Guid userId); 

    Task UpdateAsync(Item entity);

    Task<bool> ExistsByNameAsync(string name);
}