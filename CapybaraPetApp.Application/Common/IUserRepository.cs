using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Common;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> GetByIdAsync(Guid id);

    Task UpdateAsync(User entity);
}