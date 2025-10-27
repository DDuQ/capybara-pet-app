using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.AchievementAggregate;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;

public class UnlockUserAchievementCommandHandler : ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>>
{
    private readonly IAchievementRepository _achievementRepository;
    private readonly IUserRepository _userRepository;

    public UnlockUserAchievementCommandHandler(IUserRepository userRepository,
        IAchievementRepository achievementRepository)
    {
        _userRepository = userRepository;
        _achievementRepository = achievementRepository;
    }


    public async Task<ErrorOr<Success>> Handle(UnlockUserAchievementCommand command,
        CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(command.AchievementId);

        if (achievement is null) return AchievementErrors.NotFound;

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null) return UserErrors.NotFound;

        var userAchievement = new UserAchievement(command.UserId, command.AchievementId);

        user.UnlockAchievement(command.AchievementId);

        return Result.Success;
    }
}