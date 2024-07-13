using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class CapybaraRepository : Repository<Capybara>, ICapybaraRepository
{
    private readonly CapybaraPetAppDbContext _dbContext;

    public CapybaraRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Capybara>> GetCapybarasByUserIdAsync(Guid userId)
    {
        return await _dbContext
            .Set<Capybara>()
            .Where(capybara => capybara.UserId == userId)
            .Include(capybara => capybara.Interactions)
            .ToListAsync();
    }
}