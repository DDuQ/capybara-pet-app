using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.InteractionAggregate;

public class Interaction : AggregateRoot
{
    public Interaction(InteractionDetail interactionDetail, Guid avatarId)
    {
        InteractionDetail = interactionDetail;
        AvatarId = avatarId;
    }

    private Interaction() { }

    public InteractionDetail InteractionDetail { get; set; } 
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public Guid AvatarId { get; set; }
}