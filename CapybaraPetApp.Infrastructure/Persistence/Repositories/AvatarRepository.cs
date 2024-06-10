using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AvatarAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AvatarRepository(CapybaraPetAppDbContext dbContext) : IRepository<Avatar>
{
    private readonly CapybaraPetAppDbContext _dbContext = dbContext;

    public async Task Add(Avatar entity)
    {
        await _dbContext.Avatar.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Avatar?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Avatar.FirstOrDefaultAsync(avatar => avatar.Id == id);
    }

    public async Task Update(Avatar entity)
    {
        _dbContext.Avatar.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
