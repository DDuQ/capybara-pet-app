using CapybaraPetApp.Application.Abstractions.CQRS;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;

public record UnlockUserAchievementCommand(Guid AchievementId, Guid UserId) : ICommand<ErrorOr<Success>>;