namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction;

public class InteractionHistory : Entity
{
    public InteractionHistory(
        Guid userId,
        Guid capybaraId) : base(Guid.NewGuid())
    {
        UserId = userId;
        CapybaraId = capybaraId;
        InteractedAt = DateTimeOffset.UtcNow;
    }

    private InteractionHistory()
    {
    } // For EF Core

    public DateTimeOffset InteractedAt { get; private set; }
    public Guid UserId { get; set; }
    public Guid CapybaraId { get; set; }
}