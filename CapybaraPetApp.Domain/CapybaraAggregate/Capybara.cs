using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public class Capybara : AggregateRoot
{
    private readonly List<Interaction> _interactions = new();
    private readonly CapybaraStats _stats = CapybaraStats.Empty();
    public string Name { get; set; }
    public Guid? UserId { get; set; }
    public IReadOnlyCollection<Interaction> Interactions => _interactions;
    public CapybaraStats Stats => _stats;

    public Capybara(
        string name,
        CapybaraStats? stats = null,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
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

        _interactions.Add(interaction);
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

    public ErrorOr<Success> AddUserId(Guid userId)
    {
        if (UserId == userId)
        {
            return Error.Conflict(description: "Capybara has been already assigned to this user.");
        }

        UserId = userId;
        return Result.Success;
    }

    public void AddInteraction(Interaction interaction)
    {
        _interactions.Add(interaction);
    }
}