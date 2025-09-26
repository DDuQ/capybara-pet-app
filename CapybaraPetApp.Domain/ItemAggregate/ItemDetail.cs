using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public record ItemDetail
{
    public ItemDetail(ItemType itemType, int bonusEffect = 1)
    {
        ItemType = itemType;
        BonusEffect = bonusEffect;
    }

    private ItemDetail() { }

    public ItemType ItemType { get; }
    public int BonusEffect { get; }

    public static ErrorOr<Success> Validate(int quantity, int bonusEffect)
    {
        if (quantity > 100)
        {
            return ItemErrors.QuantityCannotBeGreaterThan100;
        }
        if (quantity * bonusEffect > 100)
        {
            return ItemErrors.QuantityTimesBonusEffectCannotSurpass100;
        }

        return Result.Success;
    }
}