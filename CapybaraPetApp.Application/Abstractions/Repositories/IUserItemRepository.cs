using CapybaraPetApp.Domain.Common.JoinTables;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IUserItemRepository
{
    Task<List<UserItem>> GetAllByUserIdAsync(Guid userId);
    void UpdateAsync(UserItem userItem);
}