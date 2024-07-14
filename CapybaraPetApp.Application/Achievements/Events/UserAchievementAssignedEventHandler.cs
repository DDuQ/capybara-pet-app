using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;
using MediatR;

namespace CapybaraPetApp.Application.Achievements.Events;

public class UserAchievementAssignedEventHandler : INotificationHandler<UserAchievementAssignedEvent>
{
    private readonly IAchievementRepository _achievementRepository;

    public UserAchievementAssignedEventHandler(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task Handle(UserAchievementAssignedEvent notification, CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(notification.UserAchievement.AchievementId);
        achievement?.AssignUserAchievement(notification.UserAchievement);
        await _achievementRepository.UpdateAsync(achievement!);
    }
}
