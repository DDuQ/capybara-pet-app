using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public record ItemDetail
{
    public ItemDetail(ItemType itemType, int quantity, int bonusEffect = 1)
    {
        ItemType = itemType;
        BonusEffect = bonusEffect;
        Quantity = quantity;
    }

    private ItemDetail() { }

    public ItemType ItemType { get; set; }
    public int BonusEffect { get; set; }
    public int Quantity { get; set; }

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

    internal int CalculatedQuantityPerBonusEffect => BonusEffect * Quantity;
}