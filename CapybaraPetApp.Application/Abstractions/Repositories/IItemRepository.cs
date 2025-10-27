using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IItemRepository : IRepository<Item>
{
    Task<bool> ExistsByNameAsync(string name);
    Task<List<Item>> GetAllAsync(Guid userId);
    Task<List<Item>> GetByIdsAsync(List<Guid> ids);
}