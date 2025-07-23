using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Commands;

public record CreateAchievementCommand(string Title, string Description, int Points, Rarity Rarity) : ICommand<ErrorOr<Guid>>;
