using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Commands;

public record CreateAchievementCommand(AchievementType AchievementType) : ICommand<ErrorOr<Achievement>>;
