using ErrorOr;

namespace CapybaraPetApp.Domain.InteractionAggregate;

public record InteractionDetail
{
    private const int AnHour = 3600;

    public InteractionDetail(InteractionType interactionType, int quantity)
    {
        InteractionType = interactionType;
        Quantity = quantity;
    }

    private InteractionDetail() { }

    public InteractionType InteractionType { get; private set; }
    public int Quantity { get; private set; }

    public static ErrorOr<int> ValidateQuantity(InteractionType interactionType, int quantity)
    {
        if (quantity <= 0)
        {
            return InteractionErrors.QuantityCannotBeLessThanOne;
        }

        switch (interactionType)
        {
            case InteractionType.Feed:
                if (quantity > 100)
                {
                    return InteractionErrors.TooMuchFruit;
                }
                break;

            case InteractionType.Play:
                if (InteractionTakesMoreThan(AnHour, quantity))
                {
                    return InteractionErrors.CannotTakeMoreThanAnHour;
                }
                break;

            case InteractionType.Clean:
                if (InteractionTakesMoreThan(AnHour, quantity))
                {
                    return InteractionErrors.CannotTakeMoreThanAnHour;
                }
                break;

            default:
                return InteractionErrors.UnrecognizedInteractionType;
        }

        return quantity;
    }

    private static bool InteractionTakesMoreThan(int frameOfTime, int quantity) => quantity > frameOfTime;
}