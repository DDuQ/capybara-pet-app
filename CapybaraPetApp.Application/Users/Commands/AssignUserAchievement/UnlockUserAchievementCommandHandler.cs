using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;

public class UnlockUserAchievementCommandHandler : ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAchievementRepository _achievementRepository;

    public UnlockUserAchievementCommandHandler(IUserRepository userRepository, IAchievementRepository achievementRepository)
    {
        _userRepository = userRepository;
        _achievementRepository = achievementRepository;
    }


    public async Task<ErrorOr<Success>> Handle(UnlockUserAchievementCommand command, CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(command.AchievementId);

        if (achievement is null)
        {
            return Error.NotFound(description: "Achievement does not exists."); //TODO: Add error code to Domain (AchievementErrors).
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User does not exists."); //TODO: Add error code to Domain (UserErrors).
        }

        var userAchievement = new UserAchievement(command.UserId, command.AchievementId);

        user.UnlockAchievement(command.AchievementId);

        return Result.Success;
    }
}
