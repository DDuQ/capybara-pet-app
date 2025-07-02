using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class CapybaraRepository : Repository<Capybara>, ICapybaraRepository
{
    private readonly DbSet<Capybara> _capybara;

    public CapybaraRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _capybara = dbContext.Capybara;
    }

    public async Task<List<Capybara>> GetCapybarasByUserIdAsync(Guid userId)
    {
        return await _capybara
            .Where(capybara => capybara.OwnerId == userId)
            .Include(capybara => capybara.Interactions)
            .ToListAsync();
    }
}