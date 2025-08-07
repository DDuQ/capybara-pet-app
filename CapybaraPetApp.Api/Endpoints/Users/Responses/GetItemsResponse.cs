using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Api.Endpoints.Users.Responses;

public class ItemResponse
{
    public readonly Item Item;
    public readonly int Amount;

    public ItemResponse(Item item, int amount)
    {
        Item = item;
        Amount = amount;
    }
}

public class GetItemsResponse
{
    public readonly List<ItemResponse> Items;
}

public static class GetItemsResponseMapper
{
    public static GetItemsResponse MapToResponse(this List<UserItem> userItems)
    {
        var response = new GetItemsResponse();
        userItems.ForEach(ui => response.Items.Add(new ItemResponse(ui.Item, ui.Amount)));
        
        return response;
    }
}