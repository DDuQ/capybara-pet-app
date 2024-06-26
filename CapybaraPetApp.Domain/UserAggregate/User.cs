using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public class User : AggregateRoot
{
    private readonly List<Guid> _CapybaraIds = [];
    private readonly List<Guid> _achievementsIds = [];
    private readonly List<Guid> _ItemIds = [];
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    private readonly string _passwordHash = null!;
    public IReadOnlyCollection<Guid> CapybaraIds => _CapybaraIds;
    public IReadOnlyCollection<Guid> AchievementIds => _achievementsIds;
    public IReadOnlyCollection<Guid> Items => _ItemIds;

    private User() { }

    public ErrorOr<Success> AddAchievement(Guid achievementId)
    {
        if (_achievementsIds.Contains(achievementId))
        {
            return Error.Conflict(description: "Achievement already added to User.");
        }

        _achievementsIds.Add(achievementId);
        return Result.Success;
    }

    public ErrorOr<Success> AddCapybara(Guid capybaraId)
    {
        if (_CapybaraIds.Contains(capybaraId))
        {
            return Error.Conflict(description: "Avatar already added to User.");
        }

        _CapybaraIds.Add(capybaraId);
        return Result.Success;
    }

    public ErrorOr<Success> AddItem(Guid itemId)
    {
        if (_ItemIds.Contains(itemId))
        {
            return Error.Conflict(description: "Item already added to User.");
        }

        _ItemIds.Add(itemId);
        return Result.Success;
    }
}