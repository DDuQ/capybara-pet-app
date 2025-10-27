using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate;

public static class ItemErrors
{
    public static Error InsufficientItems = Error.Conflict(
        $"{nameof(Item)}.{nameof(InsufficientItems)}",
        "Insufficient items.");

    public static Error NotFound = Error.NotFound(
        $"{nameof(Item)}.{nameof(NotFound)}",
        "Item not found.");

    public static Error ItemAlreadyExists = Error.Conflict(
        $"{nameof(Item)}.{nameof(ItemAlreadyExists)}",
        "Item already exists.");

    public static Error QuantityCannotBeGreaterThan100 = Error.Validation(
        $"{nameof(Item)}.{nameof(QuantityCannotBeGreaterThan100)}",
        "Cannot create a ItemDetail with quantity being greater than 100.");

    public static Error QuantityTimesBonusEffectCannotSurpass100 = Error.Validation(
        $"{nameof(Item)}.{nameof(QuantityTimesBonusEffectCannotSurpass100)}",
        "Cannot create a ItemDetail where Quantity * BonusEffect surpasses 100.");

    public static Error QuantityCannotBeLessThanOne = Error.Validation(
        $"{nameof(Item)}.{nameof(QuantityCannotBeLessThanOne)}",
        "Quantity cannot be less than 1.");
}