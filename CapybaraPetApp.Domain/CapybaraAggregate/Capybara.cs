using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.InteractionAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public class Capybara : AggregateRoot
{
    private readonly List<Guid> _interactionIds = [];
    private readonly CapybaraStats _stats = CapybaraStats.Empty();
    public string Name { get; set; } = null!;
    public Guid UserId { get; set; }
    public IReadOnlyList<Guid> InteractionIds => _interactionIds;

    public Capybara(
        string name,
        Guid userId,
        CapybaraStats? stats = null,
        Guid? id = null) 
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        UserId = userId;
        _stats = stats ?? CapybaraStats.Empty();
    }

    private Capybara() { }

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