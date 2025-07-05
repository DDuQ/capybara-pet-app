using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserItem
{
    public UserItem(Guid userId, Guid itemId)
    {
        UserId = userId;
        ItemId = itemId;
    }

    const int MaxAmount = 999;
    private UserItem() { }

    public int Amount { get; private set; } = 0;
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }

    public ErrorOr<Success> Add(int amount)
    {
        if (amount < 1)
        {
            return Error.Conflict(description: "Should add at least 1 or more.");
        }

        if (amount > MaxAmount)
        {
            return Error.Conflict(description: $"Cannot add beyond the limit ({MaxAmount}).");
        }

        Amount = amount;
        return Result.Success;
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
