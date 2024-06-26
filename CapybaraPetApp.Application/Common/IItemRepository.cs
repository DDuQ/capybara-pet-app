using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Application.Common;

public interface IItemRepository
{
    Task AddAsync(Item item);

    Task<Item?> GetByIdAsync(Guid id);

    Task UpdateAsync(Item entity);
}