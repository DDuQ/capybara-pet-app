using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.CapybaraAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class CapybaraRepository(CapybaraPetAppDbContext dbContext)
    : Repository<Capybara>(dbContext), ICapybaraRepository
{
    private readonly DbSet<Capybara> _capybaras = dbContext.Capybara;

    public async Task<List<Capybara>> GetCapybarasByUserIdAsync(Guid userId)
    {
        return await _capybaras
            .Where(capybara => capybara.OwnerId == userId)
            .ToListAsync();
    }
}