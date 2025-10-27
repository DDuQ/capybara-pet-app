using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public class Capybara : AggregateRoot
{
    public Capybara(
        string name,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Stats = CapybaraStats.InitialState();
    }

    private Capybara()
    {
    }

    public string Name { get; set; }
    public Guid? OwnerId { get; set; }
    public CapybaraStats Stats { get; }

    public void ReactToInteraction(IInteractionStrategy interaction, int quantity)
    {
        interaction.Apply(this, quantity);
    }

    public ErrorOr<Success> SetOwner(Guid userId)
    {
        if (OwnerId is not null) return CapybaraErrors.AlreadyAssigned;

        OwnerId = userId;
        return Result.Success;
    }

    public void UpdateStats(CapybaraStats stats)
    {
        Stats.Update(stats);
    }
}