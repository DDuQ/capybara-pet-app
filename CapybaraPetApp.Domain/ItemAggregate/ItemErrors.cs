using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public static class ItemErrors
{
    public static Error QuantityCannotBeGreaterThan100 = Error.Validation(
        "Item.QuantityCannotBeGreaterThan100",
        "Cannot create a ItemDetail with quantity being greater than 100.");

    public static Error QuantityTimesBonusEffectCannotSurpass100 = Error.Validation(
        "Item.QuantityTimesBonusEffectCannotSurpass100",
        "Cannot create a ItemDetail where Quantity * BonusEffect surpasses 100.");
}