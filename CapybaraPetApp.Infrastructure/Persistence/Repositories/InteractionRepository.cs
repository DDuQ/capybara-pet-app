using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.InteractionAggregate;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class InteractionRepository : Repository<Interaction>, IInteractionRepository
{
    public InteractionRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
    }
}