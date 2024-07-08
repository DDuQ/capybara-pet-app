using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction;

public class Interaction
{
    public Interaction(
        InteractionDetail interactionDetail,
        Guid capybaraId,
        Guid userId)
    {
        InteractionDetail = interactionDetail;
        CapybaraId = capybaraId;
        UserId = userId;
    }

    private Interaction() { }

    public InteractionDetail InteractionDetail { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid CapybaraId { get; set; }
    public Capybara Capybara { get; set; }
}