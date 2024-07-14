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


    const int MaxAmount = 999;
    private UserItem() { }

    public int Amount { get; private set; } = 0;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ItemId { get; set; }
    public Item Item { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ErrorOr<Success> AddAmount(int amount)
    {
        if (amount < 1)
        {
            return Error.Conflict(description: "Amount should be at least 1 or more.");
        }

        if (amount > MaxAmount)
        {
            return Error.Conflict(description: $"Amount cannot surpass the limit ({MaxAmount}).");
        }

        Amount = amount;
        return Result.Success;
    }

    public ErrorOr<Success> SubstractAmount(int amount)
    {
        if (Amount - amount <= 0)
        {
            return Error.Conflict(description: "There are not enough instances of this item to use.");
        }

        Amount -= amount;
        return Result.Success;
    }
}
