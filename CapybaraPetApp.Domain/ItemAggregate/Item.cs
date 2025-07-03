using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : AggregateRoot
{
    public string Name { get; set; }
    public ItemDetail ItemDetail { get; set; }

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
}