using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : AggregateRoot
{
    public Item(
        string name,
        ItemDetail itemDetail,
        Guid? id = null) :
        base(id ?? Guid.NewGuid())
    {
        Name = name;
        ItemDetail = itemDetail;
    }

    private Item()
    {
    }

    public string Name { get; init; }
    public ItemDetail ItemDetail { get; init; }
}