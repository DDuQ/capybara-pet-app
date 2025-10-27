using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmail(string email);
    Task<List<User>> GetAllAsync();
}