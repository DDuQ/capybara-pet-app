using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Achievements.Events;

public class UserAchievementAssignedEventHandler : IDomainEventHandler<UserAchievementAssignedEvent>
{
    private readonly IAchievementRepository _achievementRepository;

    public UserAchievementAssignedEventHandler(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task Handle(UserAchievementAssignedEvent domainEvent, CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(domainEvent.UserAchievement.AchievementId);
        achievement?.AssignUserAchievement(domainEvent.UserAchievement);
        await _achievementRepository.UpdateAsync(achievement!);
    }
}
