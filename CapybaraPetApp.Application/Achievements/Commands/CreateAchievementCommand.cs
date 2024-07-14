using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Achievements.Commands;

public record CreateAchievementCommand(AchievementType AchievementType) : IRequest<ErrorOr<Achievement>>;
