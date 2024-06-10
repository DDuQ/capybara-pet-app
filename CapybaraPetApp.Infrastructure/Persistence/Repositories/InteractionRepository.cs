using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.InteractionAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class InteractionRepository(CapybaraPetAppDbContext dbContext) : IRepository<Interaction>
{
    private readonly CapybaraPetAppDbContext _dbContext = dbContext;

    public async Task Add(Interaction entity)
    {
        await _dbContext.Interaction.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Interaction?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Interaction.FirstOrDefaultAsync(interaction => interaction.Id == id);
    }

    public async Task Update(Interaction entity)
    {
        _dbContext.Interaction.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
