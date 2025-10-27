using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetItems;

public class GetItemsQueryHandler(
    IItemRepository itemRepository,
    IUserRepository userRepository,
    IUserItemRepository userItemRepository)
    : IQueryHandler<GetUserItemsQuery, ErrorOr<List<InventoryItemDto>>>
{
    public async Task<ErrorOr<List<InventoryItemDto>>> Handle(GetUserItemsQuery query, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByIdAsync(query.UserId);

        if (user is null) return UserErrors.NotFound;

        var userItems = await userItemRepository.GetAllByUserIdAsync(query.UserId);
        var itemIds = userItems.Select(ui => ui.ItemId).ToList();
        var items = await itemRepository.GetByIdsAsync(itemIds);

        var inventory = user.UserItems.Join(items, ui => ui.ItemId, item => item.Id,
            (ui, item) => new InventoryItemDto
            {
                ItemId = item.Id,
                ItemName = item.Name,
                ItemDescription = $"Type: {item.ItemDetail.ItemType} | Bonus effect: {item.ItemDetail.BonusEffect} %",
                Quantity = ui.Quantity
            });
        
        return inventory.ToList();
    }
}