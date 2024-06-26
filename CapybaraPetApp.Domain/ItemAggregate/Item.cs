using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : AggregateRoot
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public ItemDetail ItemDetail { get; set; }

    public Item(string name, ItemDetail itemDetail, Guid userId)
    {
        UserId = userId;
        Name = name;
        ItemDetail = itemDetail;
    }

    private Item() { }

    public ErrorOr<Success> UseItem(int amount)
    {
        if (Amount - amount <= 0)
        {
            return Error.Conflict(description: $"There are not enough instances of this item to use.");
        }

        Amount -= amount;
        return Result.Success;
    }
}