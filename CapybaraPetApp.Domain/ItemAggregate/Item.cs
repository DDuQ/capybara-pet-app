using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class Item : Entity
{
    private List<User> _users { get; set; } = new();
    public string Name { get; set; }
    public int Amount { get; set; } = 0;
    public ItemDetail ItemDetail { get; set; }

    public IReadOnlyCollection<User> Users => _users.ToList();

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

    public ErrorOr<Success> UseItem(int amount)
    {
        if (Amount - amount <= 0)
        {
            return Error.Conflict(description: "There are not enough instances of this item to use.");
        }

        Amount -= amount;
        return Result.Success;
    }

    public ErrorOr<Success> AddUser(User user)
    {
        if (_users.Contains(user))
        {
            return Error.Conflict(description: "User is already related to this item.");
        }

        _users.Add(user);
        return Result.Success;
    }
}