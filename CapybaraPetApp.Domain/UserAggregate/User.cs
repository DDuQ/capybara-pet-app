using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate.Events;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public class User : AggregateRoot
{
    public readonly List<UserAchievement> _userAchievements = [];
    public readonly List<Capybara> _capybaras = [];
    public readonly List<Interaction> _interactions = [];
    public readonly List<UserItem> _userItems = [];
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    private readonly string _passwordHash = null!;
    public IReadOnlyCollection<UserAchievement> UserAchievements => _userAchievements.ToList();
    public IReadOnlyCollection<Capybara> Capybaras => _capybaras.ToList();
    public IReadOnlyCollection<Interaction> Interactions => _interactions.ToList();
    public IReadOnlyCollection<UserItem> UserItems => _userItems.ToList();

    public User(
        string username,
        string email,
        Guid? id) :
        base(id ?? Guid.NewGuid())
    {
        Username = username;
        Email = email;
    }

    public User() { }

    public ErrorOr<Success> AssignUserAchievement(UserAchievement userAchievement)
    {
        if (_userAchievements.Any(ua => ua.AchievementId == userAchievement.AchievementId) &&
            _userAchievements.Any(ua => ua.UserId == userAchievement.UserId))
        {
            return Error.Conflict(description: "User achievement has been assigned already.");
        }

        _userAchievements.Add(userAchievement);
        _domainEvents.Add(new UserAchievementAssignedEvent(userAchievement));
        return Result.Success;
    }

    public ErrorOr<Success> AssignCapybara(Capybara capybara)
    {
        if (_capybaras.Any(c => c.Id == capybara.Id))
        {
            return Error.Conflict(description: "Avatar already added to User.");
        }

        _capybaras.Add(capybara);
        _domainEvents.Add(new CapybaraAssignedEvent(capybara, Id));
        return Result.Success;
    }

    public ErrorOr<Success> AssignItem(Item item)
    {
        if (_userItems.Exists(it => it.ItemId == item.Id && it.UserId == Id))
        {
            return Error.Conflict(description: "UserItem already added to User.");
        }

        var userItem = new UserItem(Id, item.Id);
        _userItems.Add(userItem);
        _domainEvents.Add(new ItemAssignedEvent(item, this));
        return Result.Success;
    }

    public void AddInteraction(Interaction interaction)
    {
        _interactions.Add(interaction);
        _domainEvents.Add(new InteractionCreatedEvent(interaction));
    }

    public bool OwnsCapybara(Capybara capybara) => _capybaras.Contains(capybara);
}