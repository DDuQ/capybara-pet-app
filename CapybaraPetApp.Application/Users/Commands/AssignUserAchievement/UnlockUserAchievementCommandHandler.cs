using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.AchievementAggregate;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;

public class UnlockUserAchievementCommandHandler(
    IUserRepository userRepository,
    IAchievementRepository achievementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UnlockUserAchievementCommand command,
        CancellationToken cancellationToken)
    {
        var achievement = await achievementRepository.GetByIdAsync(command.AchievementId);

        if (achievement is null) return AchievementErrors.NotFound;

        var user = await userRepository.GetByIdAsync(command.UserId);

        if (user is null) return UserErrors.NotFound;

        var userAchievement = new UserAchievement(command.UserId, command.AchievementId);

        user.UnlockAchievement(command.AchievementId);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}