using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.UserAggregate;

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
    }

    private Interaction() { }

    public InteractionDetail InteractionDetail { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CapybaraId { get; set; }
    public Capybara Capybara { get; set; }
}