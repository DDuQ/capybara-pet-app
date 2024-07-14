using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Achievements.Commands;

public class CreateAchievementCommandHandler : IRequestHandler<CreateAchievementCommand, ErrorOr<Achievement>>
{
    private readonly IAchievementRepository _achievementRepository;

    public CreateAchievementCommandHandler(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task<ErrorOr<Achievement>> Handle(CreateAchievementCommand request, CancellationToken cancellationToken)
    {
        if (await _achievementRepository.ExistsByNameAsync(request.AchievementType.Name))
        {
            return Error.Conflict(description: $"Item {request.AchievementType.Name} already exists.");
        }

        var achievement = new Achievement(request.AchievementType);

        await _achievementRepository.AddAsync(achievement);

        return achievement;
    }
}
