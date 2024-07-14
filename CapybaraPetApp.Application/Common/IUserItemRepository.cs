using CapybaraPetApp.Domain.Common.JoinTables;

namespace CapybaraPetApp.Application.Common;

public interface IUserItemRepository
{
    Task<UserItem?> GetByIdsAsync(Guid userId, Guid itemId);
    Task UpdateAsync(UserItem userItem);
}
