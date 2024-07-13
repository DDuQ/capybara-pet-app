using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddUserAchievement;

public record AddUserAchievementCommand(Guid AchievementId, Guid UserId) : IRequest<ErrorOr<Success>>;
