namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserCapybara
{
    public UserCapybara(Guid userId, Guid capybaraId)
    {
        UserId = userId;
        CapybaraId = capybaraId;
        AdoptedAt = DateTimeOffset.UtcNow;
    }

    private UserCapybara() // For EF Core
    {
    }
 
    public Guid UserId { get; set; }
    public Guid CapybaraId { get; set; }
    public DateTimeOffset AdoptedAt { get; set; }
}