using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item(string name, ItemDetail itemDetail, Guid userId)
{
    public Guid Id { get; set; } = new Guid();
    public Guid UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public int Amount { get; set; }
    public ItemDetail ItemDetail { get; set; } = itemDetail;

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