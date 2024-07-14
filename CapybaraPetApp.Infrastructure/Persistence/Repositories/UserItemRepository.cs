using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserItemRepository : IUserItemRepository
{
    private readonly DbSet<UserItem> _userItem;
    private readonly CapybaraPetAppDbContext _dbContext;

    public UserItemRepository(CapybaraPetAppDbContext dbContext)
    {
        _userItem = dbContext.UserItem;
        _dbContext = dbContext;
    }

    public async Task<UserItem?> GetByIdsAsync(Guid userId, Guid itemId)
    {
        return await _userItem.FirstOrDefaultAsync(userItem => userItem.UserId == userId && userItem.ItemId == itemId);
    }

    public async Task UpdateAsync(UserItem userItem)
    {
        _userItem.Update(userItem);
        await _dbContext.SaveChangesAsync();
    }
}
