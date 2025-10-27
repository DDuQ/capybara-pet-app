using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Queries;

public record GetAchievementQuery(Guid Id) : IQuery<ErrorOr<Achievement>>;