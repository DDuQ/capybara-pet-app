using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddUserAchievement;

public record AssignUserAchievementCommand(Guid AchievementId, Guid UserId) : IRequest<ErrorOr<Success>>;
