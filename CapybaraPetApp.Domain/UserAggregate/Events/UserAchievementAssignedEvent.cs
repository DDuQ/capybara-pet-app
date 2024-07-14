using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using MediatR;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record UserAchievementAssignedEvent(UserAchievement UserAchievement) : IDomainEvent;
