using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public class ItemDetail(ItemType itemType, int quantity, int bonusEffect = 1) : ValueObject
{
    public ItemType ItemType { get; set; } = itemType;
    public int BonusEffect { get; set; } = bonusEffect;
    public int Quantity { get; set; } = quantity;

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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ItemType;
        yield return BonusEffect;
        yield return Quantity;
    }
}