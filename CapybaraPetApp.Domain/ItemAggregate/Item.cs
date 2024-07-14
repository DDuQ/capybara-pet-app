using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.Common.Mappings;
using CapybaraPetApp.Domain.ItemAggregate.Events;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : AggregateRoot
{

    private readonly List<UserItem> _userItems = new();
    public string Name { get; set; }
    public ItemDetail ItemDetail { get; set; }
    public IReadOnlyCollection<UserItem> UserItems => _userItems.ToList();

    public Item(
        string name,
        ItemDetail itemDetail,
        Guid? id = null) : 
        base(id ?? Guid.NewGuid())
    {
        Name = name;
        ItemDetail = itemDetail;
    }

    private Item() { }

    public ErrorOr<Success> UseItem(Guid userId, Guid capybaraId, int amount)
    {
        if (!_userItems.Any(it => it.ItemId == Id && it.UserId == userId))
        {
            return Error.NotFound(description: "UserItem not found in item.");
        }

        var userItem = _userItems.First(it => it.ItemId == Id && it.UserId == userId);

        userItem.SubstractAmount(amount);

        var interactionTypeResult = ItemInteractionMapping.TryGetInteractionType(ItemDetail.ItemType);

        if (interactionTypeResult.IsError)
        {
            return interactionTypeResult.Errors;
        }

        var interactionDetail = new InteractionDetail(interactionTypeResult.Value, amount);

        _domainEvents.Add(new ItemUsedEvent(userId, capybaraId, interactionDetail, userItem));
        return Result.Success;
    }

    public ErrorOr<Success> AssignUser(User user)
    {
        if (_userItems.Exists(it => it.ItemId == Id && it.UserId == user.Id))
        {
            return Error.Conflict(description: "UserItem already added to Item.");
        }

        var userItem = new UserItem(user.Id, Id);
        _userItems.Add(userItem);
        return Result.Success;
    }
}