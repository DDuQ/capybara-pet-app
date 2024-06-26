using ErrorOr;

namespace CapybaraPetApp.Domain.InteractionAggregate;

public static class InteractionErrors
{
    public static readonly Error CannotTakeMoreThanAnHour = Error.Validation(
        "Interaction.CannotTakeMoreThanAnHour",
        "Interaction duration is too long.");

    public static readonly Error TooMuchFruit = Error.Validation(
        "Interaction.TooMuchFruit",
        "Fruit quantity is too much for Capy to handle.");

    public static readonly Error UnrecognizedInteractionType = Error.Validation(
        "Interaction.UnrecognizedInteractionType",
        "Interaction Type does not exists.");

    public static readonly Error QuantityCannotBeLessThanOne = Error.Validation(
        "Interaction.QuantityCannotBeLessThanOne",
        "Quantity must be a positive greater than 0.");
}