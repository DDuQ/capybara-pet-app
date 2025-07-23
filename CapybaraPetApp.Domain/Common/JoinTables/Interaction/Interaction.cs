namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction;

public class Interaction : Entity
{
    public Interaction(
        InteractionDetail interactionDetail,
        Guid userId,
        Guid capybaraId) : base(Guid.NewGuid())
    {
        InteractionDetail = interactionDetail;
        UserId = userId;
        CapybaraId = capybaraId;
        InteractedAt = DateTimeOffset.UtcNow;
    }

    private Interaction() { }

    public InteractionDetail InteractionDetail { get; private set; }
    public DateTimeOffset InteractedAt { get; private set; }
    public Guid UserId { get; set; }
    public Guid CapybaraId { get; set; }
}