using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : AggregateRoot
{
    public string Name { get; init; }
    public ItemDetail ItemDetail { get; init; }
    public int Quantity { get; private set; }

    public Item(
        string name,
        ItemDetail itemDetail,
        int quantity,
        Guid? id = null) :
        base(id ?? Guid.NewGuid())
    {
        Name = name;
        ItemDetail = itemDetail;
        Quantity = quantity;
    }

    private Item() { }
    
    public ErrorOr<Success> Use(int quantity)
    {
        if (Quantity - quantity < 1)
        {
            return ItemErrors.InsufficientItems;
        }

        Quantity -= quantity;
        return Result.Success;
    }

    public ErrorOr<Success> Add(int quantity)
    {
        if (quantity < 1)
        {
            return ItemErrors.QuantityCannotBeLessThanOne;
        }

        Quantity += quantity;
        return Result.Success;
    }
}