using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public class Capybara : AggregateRoot
{
    private readonly CapybaraStats _stats;
    public string Name { get; set; }
    public Guid? OwnerId { get; set; }
    public CapybaraStats Stats => _stats;
    
    public Capybara(
        string name,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _stats = CapybaraStats.InitialState();
    }

    private Capybara() { }

    public void ReactToInteraction(IInteractionStrategy interaction, int quantity) 
        => interaction.Apply(this, quantity);
    
    public ErrorOr<Success> SetOwner(Guid userId)
    {
        if (OwnerId == userId)
        {
            return CapybaraErrors.AlreadyAssigned;
        }

        OwnerId = userId;
        return Result.Success;
    }
    
    public void UpdateStats(CapybaraStats stats)
    {
        _stats.Update(stats);
    } 
}