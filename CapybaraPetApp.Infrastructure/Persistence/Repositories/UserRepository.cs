using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserRepository(CapybaraPetAppDbContext dbContext) : IRepository<User>
{
    private readonly CapybaraPetAppDbContext _dbContext = dbContext;

    public async Task Add(User entity)
    {
        await _dbContext.User.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.User.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task Update(User entity)
    {
        _dbContext.User.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
