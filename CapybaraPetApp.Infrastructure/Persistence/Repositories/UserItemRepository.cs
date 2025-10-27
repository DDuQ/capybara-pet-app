using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.Common.JoinTables;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserItemRepository(CapybaraPetAppDbContext dbContext) : IUserItemRepository
{
    private readonly DbSet<UserItem> _userItems = dbContext.UserItem;

    public async Task<List<UserItem>> GetAllByUserIdAsync(Guid userId)
    {
        return await _userItems.Where(userItem => userItem.UserId == userId).ToListAsync();
    }

    public void UpdateAsync(UserItem userItem)
    {
        _userItems.Update(userItem);
    }
}