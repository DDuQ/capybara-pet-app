using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserItem
{
    private const int MaxAmount = 999;

    public UserItem(Guid userId, Guid itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }

    private UserItem()
    {
    } // For EF Core

    public int Quantity { get; private set; }
    public Guid UserId { get; private set; }
    public Guid ItemId { get; private set; }
    public Item Item { get; private set; }
    public User User { get; private set; }

    public ErrorOr<Success> Add(int amount)
    {
        switch (amount)
        {
            case < 1:
                return Error.Conflict(description: "Should add at least 1 or more.");
            case > MaxAmount:
                return Error.Conflict(description: $"Cannot add beyond the limit ({MaxAmount}).");
            default:
                Quantity = amount;
                return Result.Success;
        }
    }

    public ErrorOr<Success> Use(int amount)
    {
        if (Quantity - amount <= 0)
            return Error.Conflict(description: "There are not enough instances of this item to use.");

        Quantity -= amount;
        return Result.Success;
    }
}