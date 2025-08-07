using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserItem
{
    public UserItem(Guid userId, Guid itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }

    private const int MaxAmount = 999;
    private UserItem() { } // For EF Core

    public int Amount { get; private set; } = 0;
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
                Amount = amount;
                return Result.Success;
        }
    }

    public ErrorOr<Success> Use(int amount)
    {
        if (Amount - amount <= 0)
        {
            return Error.Conflict(description: "There are not enough instances of this item to use.");
        }

        Amount -= amount;
        return Result.Success;
    }
}
