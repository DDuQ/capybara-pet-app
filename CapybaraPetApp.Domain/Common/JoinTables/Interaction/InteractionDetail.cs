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

    public static ErrorOr<Success> Validate(InteractionDetail interactionDetail)
    {
        if (interactionDetail.Quantity <= 0)
        {
            return InteractionErrors.QuantityCannotBeLessThanOne;
        }

        return interactionDetail.InteractionType switch
        {
            InteractionType.Feed when interactionDetail.Quantity > 100 =>
                InteractionErrors.TooMuchFruit,

            InteractionType.Play or InteractionType.Clean
                when TakesLongerThanAnHour(interactionDetail.Quantity) =>
                InteractionErrors.CannotTakeMoreThanAnHour,

            InteractionType.Feed or InteractionType.Play or InteractionType.Clean =>
                Result.Success,

            _ => InteractionErrors.UnrecognizedInteractionType
        };
    }

    private static bool TakesLongerThanAnHour(int expectedTime) => expectedTime > AnHour;
}