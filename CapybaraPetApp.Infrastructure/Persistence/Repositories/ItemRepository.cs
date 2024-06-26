using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class ItemRepository : Repository<Item>, IItemRepository
{
    public ItemRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
    }
}