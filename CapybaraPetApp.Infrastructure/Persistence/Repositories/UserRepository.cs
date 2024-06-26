using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
    }
}