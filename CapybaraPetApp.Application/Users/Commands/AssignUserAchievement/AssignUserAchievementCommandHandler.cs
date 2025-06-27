using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;

public class AssignUserAchievementCommandHandler : ICommandHandler<AssignUserAchievementCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAchievementRepository _achievementRepository;

    public AssignUserAchievementCommandHandler(IUserRepository userRepository, IAchievementRepository achievementRepository)
    {
        _userRepository = userRepository;
        _achievementRepository = achievementRepository;
    }


    public async Task<ErrorOr<Success>> Handle(AssignUserAchievementCommand command, CancellationToken cancellationToken)
    {
        var achievement = await _achievementRepository.GetByIdAsync(command.AchievementId);

        if (achievement is null)
        {
            return Error.NotFound(description: "Achievement does not exists.");
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User does not exists.");
        }

        var userAchievement = new UserAchievement(command.UserId, command.AchievementId);

        user.AssignUserAchievement(userAchievement);

        return Result.Success;
    }
}
