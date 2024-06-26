using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AchievementAggregare;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AchievementRepository(CapybaraPetAppDbContext dbContext) : Repository<Achievement>(dbContext), IAchievementRepository
{
}