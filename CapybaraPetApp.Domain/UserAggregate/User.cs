using ErrorOr;

namespace CapybaraPetApp.Domain.Entities;

public class User
{
    private readonly List<Guid> _avatarIds = [];
    private readonly List<Guid> _achievementsIds = [];
    private readonly List<Guid> _ItemIds = [];
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    private readonly string _passwordHash = null!;
    public IReadOnlyCollection<Guid> AvatarIds => _avatarIds;
    public IReadOnlyCollection<Guid> AchievementIds => _achievementsIds;
    public IReadOnlyCollection<Guid> Items => _ItemIds;

    public ErrorOr<Success> AddAchievement(Guid achievementId)
    {
        if (_achievementsIds.Contains(achievementId))
        {
            return Error.Conflict(description: "Achievement already added to User.");
        }

        _achievementsIds.Add(achievementId);
        return Result.Success;
    }

    public ErrorOr<Success> AddAvatar(Guid avatarId)
    {
        if (_avatarIds.Contains(avatarId))
        {
            return Error.Conflict(description: "Avatar already added to User.");
        }

        _avatarIds.Add(avatarId);
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