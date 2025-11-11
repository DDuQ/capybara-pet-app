using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.ItemAggregate.Events;
using CapybaraPetApp.Domain.UserAggregate.Events;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public class User : AggregateRoot
{
    public readonly List<InteractionHistory> _interactions = [];
    public readonly List<UserAchievement> _userAchievements = [];
    public readonly List<AdoptionHistory> _userCapybaras = [];
    public readonly List<UserItem> _userItems = [];
    internal string PasswordHash = null!; //TODO: Implement password hashing and validation.

    public User(string username, string email, Guid? id)
        : base(id ?? Guid.NewGuid())
    {
        Username = username;
        Email = email;
    }

    private User() {} 

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IReadOnlyCollection<UserAchievement> UserAchievements => _userAchievements.ToList();
    public IReadOnlyCollection<AdoptionHistory> UserCapybaras => _userCapybaras.ToList();
    public IReadOnlyCollection<UserItem> UserItems => _userItems.ToList();
    public IReadOnlyCollection<InteractionHistory> Interactions => _interactions.ToList();

    public ErrorOr<Success> UnlockAchievement(Guid achievementId)
    {
        if (_userAchievements.Any(ua => ua.AchievementId == achievementId))
            return UserErrors.AchievementAlreadyAssigned;

        _userAchievements.Add(new UserAchievement(Id, achievementId));
        return Result.Success;
    }

    public ErrorOr<Success> AdoptCapybara(Guid capybaraId)
    {
        if (_userCapybaras.Any(c => c.CapybaraId == capybaraId)) return UserErrors.CapybaraAlreadyAdopted;

        _userCapybaras.Add(new AdoptionHistory(Id, capybaraId));
        return Result.Success;
    }

    public ErrorOr<Success> BuyItem(Guid itemId) //TODO: Currency logic when buying items.
    {
        if (_userItems.Any(it => it.ItemId == itemId)) return UserErrors.ItemNotOwned;

        var userItem = new UserItem(Id, itemId);
        _userItems.Add(userItem);
        _domainEvents.Add(new ItemPurchasedEvent(itemId, Id));
        return Result.Success;
    }

    public void InteractWithCapybara(Guid capybaraId)
    {
        if (!OwnsCapybara(capybaraId))
            throw new EventualConsistencyException(Error.Conflict(description: "User does not own capybara."));

        _interactions.Add(new InteractionHistory(Id, capybaraId));
    }

    private bool OwnsCapybara(Guid capybaraId)
    {
        return _userCapybaras.Any(uc => uc.CapybaraId == capybaraId);
    }

    public ErrorOr<Success> UseItemOnCapybara(Guid itemId, Guid capybaraId, int itemAmount)
    {
        var userItem = _userItems.FirstOrDefault(it => it.ItemId == itemId);

        if (userItem is null) return UserErrors.ItemNotOwned;

        userItem.Use(itemAmount);

        _domainEvents.Add(new ApplyItemEffectToCapybaraEvent(Id, itemId, capybaraId, itemAmount));
        return Result.Success;
    }
}

public static class UserExtensions
{
    public static User AddHashedPassword(this User user, string hashedPassword)
    {
        user.PasswordHash = hashedPassword;
        return user;
    }
}