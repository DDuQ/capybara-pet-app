using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.InteractionAggregate;

public class InteractionDetail(InteractionType interactionType, int quantity) : ValueObject
{
    private const int AnHour = 3600;
    public InteractionType InteractionType { get; private set; } = interactionType;
    public int Quantity { get; private set; } = quantity;

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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return InteractionType;
        yield return Quantity;
    }
}