using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Commands;

public class CreateAchievementCommandHandler(IAchievementRepository achievementRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAchievementCommand, ErrorOr<Achievement>>
{
    public async Task<ErrorOr<Achievement>> Handle(CreateAchievementCommand command,
        CancellationToken cancellationToken)
    {
        if (await achievementRepository.ExistsByNameAsync(command.Title))
            return Error.Conflict(description: $"Item {command.Title} already exists.");

        var achievement = Achievement.Create(command.Title, command.Description, command.Points, command.Rarity);

        if (achievement.IsError) return achievement.Errors;

        await achievementRepository.AddAsync(achievement.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return achievement.Value;
    }
}