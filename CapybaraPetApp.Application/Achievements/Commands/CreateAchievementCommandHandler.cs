using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Commands;

public class CreateAchievementCommandHandler : ICommandHandler<CreateAchievementCommand, ErrorOr<Achievement>>
{
    private readonly IAchievementRepository _achievementRepository;

    public CreateAchievementCommandHandler(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task<ErrorOr<Achievement>> Handle(CreateAchievementCommand command, CancellationToken cancellationToken)
    {
        if (await _achievementRepository.ExistsByNameAsync(command.AchievementType.Name))
        {
            return Error.Conflict(description: $"Item {command.AchievementType.Name} already exists.");
        }

        var achievement = new Achievement(command.AchievementType);

        await _achievementRepository.AddAsync(achievement);

        return achievement;
    }
}
