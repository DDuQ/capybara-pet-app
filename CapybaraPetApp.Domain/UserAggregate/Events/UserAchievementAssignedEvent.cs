using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record UserAchievementAssignedEvent(UserAchievement UserAchievement) : IDomainEvent;
