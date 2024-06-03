namespace CapybaraPetApp.Domain.InteractionAggregate;

public class Interaction(InteractionDetail interactionDetail, Guid avatarId)
{
    public Guid Id { get; set; } = new Guid();
    public InteractionDetail InteractionDetail { get; set; } = interactionDetail;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public Guid AvatarId { get; set; } = avatarId;
}