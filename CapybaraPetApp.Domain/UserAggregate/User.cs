using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using CapybaraPetApp.Domain.ItemAggregate.Events;
using CapybaraPetApp.Domain.UserAggregate.Events;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public class User : AggregateRoot
{
    public readonly List<UserAchievement> _userAchievements = [];
    public readonly List<UserCapybara> _userCapybaras = [];
    public readonly List<UserItem> _userItems = [];
    public readonly List<InteractionHistory> _interactions = [];
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    internal string PasswordHash = null!; //TODO: Implement password hashing and validation.
    public IReadOnlyCollection<UserAchievement> UserAchievements => _userAchievements.ToList();
    public IReadOnlyCollection<UserCapybara> UserCapybaras => _userCapybaras.ToList();
    public IReadOnlyCollection<UserItem> UserItems => _userItems.ToList();
    public IReadOnlyCollection<InteractionHistory> Interactions => _interactions.ToList();

    public User(string username, string email, Guid? id)
        : base(id ?? Guid.NewGuid())
    {
        Username = username;
        Email = email;
    }

    private User() { }
    
    public ErrorOr<Success> UnlockAchievement(Guid achievementId)
    {
        if (_userAchievements.Any(ua => ua.AchievementId == achievementId))
        {
            return UserErrors.AchievementAlreadyAssigned;
        }

        _userAchievements.Add(new UserAchievement(Id, achievementId));
        return Result.Success;
    }

    public ErrorOr<Success> AdoptCapybara(Guid capybaraId)
    {
        if (_userCapybaras.Any(c => c.CapybaraId == capybaraId))
        {
            return UserErrors.CapybaraAlreadyAdded;
        }

        _userCapybaras.Add(new UserCapybara(Id, capybaraId));
        _domainEvents.Add(new CapybaraAdoptedEvent(capybaraId, Id));
        return Result.Success;
    }

    public ErrorOr<Success> BuyItem(Guid itemId) //TODO: Currency logic when buying items.
    {
        if (_userItems.Any(it => it.ItemId == itemId))
        {
            return UserErrors.ItemNotOwned;
        }

        var userItem = new UserItem(Id, itemId);
        _userItems.Add(userItem);
        _domainEvents.Add(new ItemPurchasedEvent(itemId, Id));
        return Result.Success;
    }

    public void InteractWithCapybara(Guid capybaraId, IInteractionStrategy interactionStrategy, int quantity)
    {
        _interactions.Add(new InteractionHistory(Id, capybaraId));
        _domainEvents.Add(new UpdateCapybaraMoodOnInteraction(capybaraId, interactionStrategy, quantity));
    }

    public bool OwnsCapybara(Guid capybaraId) => _userCapybaras.Any(uc => uc.CapybaraId == capybaraId);

    public ErrorOr<Success> UseItemOnCapybara(Guid itemId, Guid capybaraId, int itemAmount)
    {
        var userItem = _userItems.FirstOrDefault(it => it.ItemId == itemId);

        if (userItem is null)
        {
            return UserErrors.ItemNotOwned;
        }

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