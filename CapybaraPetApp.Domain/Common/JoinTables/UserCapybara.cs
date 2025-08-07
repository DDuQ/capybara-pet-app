using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserCapybara
{
    public UserCapybara(Guid userId, Guid capybaraId)
    {
        UserId = userId;
        CapybaraId = capybaraId;
        AdoptionDate = DateTimeOffset.UtcNow;
    }

    private UserCapybara() { } // For EF Core
 
    public Guid UserId { get; set; }
    public Guid CapybaraId { get; set; }
    public DateTimeOffset AdoptionDate { get; set; }
    public User User { get; private set; }
    public Capybara Capybara { get; private set; }
}