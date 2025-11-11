using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmail(string email);
    Task<UserDto?> GetAllRelatedDataByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
}