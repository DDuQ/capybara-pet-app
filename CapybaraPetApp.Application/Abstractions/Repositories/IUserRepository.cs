using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<UserDto?> GetAllRelatedDataByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<List<User>> GetAllAsync();
}