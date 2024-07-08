using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public class User : AggregateRoot
{
    public readonly List<UserAchievement> _userAchievements = new();
    public readonly List<Capybara> _capybaras = new();
    public readonly List<Interaction> _interactions = new();
    public readonly List<Item> _items = new();
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    private readonly string _passwordHash = null!;
    public IReadOnlyCollection<UserAchievement> UserAchievements => _userAchievements.ToList();
    public IReadOnlyCollection<Capybara> Capybara => _capybaras.ToList();
    public IReadOnlyCollection<Interaction> Interactions => _interactions.ToList();
    public IReadOnlyCollection<Item> Items => _items.ToList();

    private User() { }

    public ErrorOr<Success> AddUserAchievement(UserAchievement userAchievement)
    {
        if (_userAchievements.Any(ua => ua.AchievementId == userAchievement.AchievementId) &&
            _userAchievements.Any(ua => ua.UserId == userAchievement.UserId) &&
            _userAchievements.Any(ua => ua.CreatedAt == userAchievement.CreatedAt))
        {
            return Error.Conflict(description: "User achievement already exists.");
        }

        _userAchievements.Add(userAchievement);
        return Result.Success;
    }

    public ErrorOr<Success> AddCapybara(Capybara capybara)
    {
        if (_capybaras.Any(capy => capy.Id == capybara.Id))
        {
            return Error.Conflict(description: "Avatar already added to User.");
        }

        _capybaras.Add(capybara);
        return Result.Success;
    }

    public ErrorOr<Success> AddItem(Item item)
    {
        if (_items.Any(it => it.Id == item.Id))
        {
            return Error.Conflict(description: "Item already added to User.");
        }

        _items.Add(item);
        return Result.Success;
    }

    public ErrorOr<Success> AddInteraction(Interaction interaction)
    {
        if (_interactions.Any(inter => inter.UserId == interaction.UserId) &&
            _interactions.Any(inter => inter.CapybaraId == interaction.CapybaraId))
        {
            return Error.Conflict(description: "Interaction already added to User.");
        }

        _interactions.Add(interaction);
        return Result.Success;
    }
}