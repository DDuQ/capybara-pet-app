using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Achievements.Queries;

public class GetAchievementQueryHandler : IQueryHandler<GetAchievementQuery, ErrorOr<Achievement>>
{
    private readonly IAchievementRepository _achievementRepository;

    public GetAchievementQueryHandler(IAchievementRepository achievementRepository)
    {
        _achievementRepository = achievementRepository;
    }

    public async Task<ErrorOr<Achievement>> Handle(GetAchievementQuery query,
        CancellationToken cancellationToken = default)
    {
        var achievement = await _achievementRepository.GetByIdAsync(query.Id);

        return achievement is null ? AchievementErrors.NotFound : achievement;
    }
}