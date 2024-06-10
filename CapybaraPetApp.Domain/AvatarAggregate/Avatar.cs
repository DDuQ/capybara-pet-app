using CapybaraPetApp.Domain.InteractionAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.AvatarAggregate;

public class Avatar 
{
    private readonly List<Guid> _interactionIds = [];
    private readonly AvatarStats _stats = AvatarStats.Empty();
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public IReadOnlyList<Guid> InteractionIds => _interactionIds;

    public ErrorOr<Success> Interact(Interaction interaction)
    {
        switch (interaction.InteractionDetail.InteractionType)
        {
            case InteractionType.Feed:
                GiveFruits(interaction.InteractionDetail.Quantity);
                break;
            case InteractionType.Play:
                Play(interaction.InteractionDetail.Quantity);
                break;
            case InteractionType.Clean:
                BathTime(interaction.InteractionDetail.Quantity);
                break;
            default:
                return InteractionErrors.UnrecognizedInteractionType;
        }

        _interactionIds.Add(interaction.Id);
        return Result.Success;
    } 

    private ErrorOr<Success> GiveFruits(int fruitQuantity)
    {
        _stats.Feed(fruitQuantity);
        return Result.Success;
    }

    private ErrorOr<Success> BathTime(int bathTimeHours)
    {
        _stats.Clean(bathTimeHours);
        return Result.Success;
    }

    private ErrorOr<Success> Play(int playTimeHours)
    {
        _stats.Play(playTimeHours);
        return Result.Success;
    }
}