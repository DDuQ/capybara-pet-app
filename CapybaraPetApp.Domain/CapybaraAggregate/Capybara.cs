using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public class Capybara : AggregateRoot
{
    private readonly CapybaraStats _stats = CapybaraStats.Empty();
    public string Name { get; set; }
    public Guid? OwnerId { get; set; }
    public CapybaraStats Stats => _stats;

    public static ErrorOr<Capybara> Create(
        string name,
        int initialHappiness = 0,
        int initialHealth = 100,
        int initialEnergy = 100,
        Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return CapybaraErrors.NameRequired;
            
        var statsResult = CapybaraStats.Create(initialHappiness, initialHealth, initialEnergy);
        if (statsResult.IsError)
            return statsResult.Errors;
            
        return new Capybara(name, statsResult.Value, id);
    }

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

    public ErrorOr<Success> ReactToInteraction(Interaction interaction)
    {
        var detail = interaction.InteractionDetail;
        return detail.InteractionType switch
        {
            InteractionType.Feed => GiveFruits(detail.Quantity),
            InteractionType.Play => Play(detail.Quantity),
            InteractionType.Clean => BathTime(detail.Quantity),
            _ => InteractionErrors.UnrecognizedInteractionType
        };
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

    public ErrorOr<Success> SetOwner(Guid userId)
    {
        if (OwnerId == userId)
        {
            return CapybaraErrors.AlreadyAssigned;
        }

        OwnerId = userId;
        return Result.Success;
    }
}