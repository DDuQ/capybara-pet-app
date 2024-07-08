using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddAchievement;

public record AddUserAchievementCommand(Guid AchievementId, Guid UserId) : IRequest<ErrorOr<Success>>;
