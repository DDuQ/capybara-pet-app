using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction;

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

    public static ErrorOr<Success> IsInteractionDetailValid(InteractionDetail interactionDetail)
    {
        if (interactionDetail.Quantity <= 0)
        {
            return InteractionErrors.QuantityCannotBeLessThanOne;
        }

        switch (interactionDetail.InteractionType)
        {
            case InteractionType.Feed:
                if (interactionDetail.Quantity > 100)
                {
                    return InteractionErrors.TooMuchFruit;
                }
                break;

            case InteractionType.Play:
                if (InteractionTakesMoreThan(AnHour, interactionDetail.Quantity))
                {
                    return InteractionErrors.CannotTakeMoreThanAnHour;
                }
                break;

            case InteractionType.Clean:
                if (InteractionTakesMoreThan(AnHour, interactionDetail.Quantity))
                {
                    return InteractionErrors.CannotTakeMoreThanAnHour;
                }
                break;

            default:
                return InteractionErrors.UnrecognizedInteractionType;
        }

        return Result.Success;
    }

    private static bool InteractionTakesMoreThan(int frameOfTime, int quantity) => quantity > frameOfTime;
}