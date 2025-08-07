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
        if (await _achievementRepository.ExistsByNameAsync(command.Title))
        {
            return Error.Conflict(description: $"Item {command.Title} already exists.");
        }

        var achievement = Achievement.Create(command.Title, command.Description, command.Points, command.Rarity);
        
        if (achievement.IsError)
        {
            return achievement.Errors;
        }
        
        await _achievementRepository.AddAsync(achievement.Value);

        return achievement.Value;
    }
}
