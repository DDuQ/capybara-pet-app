using CapybaraPetApp.Domain.CapybaraAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface ICapybaraRepository : IRepository<Capybara>
{
    Task<List<Capybara>> GetCapybarasByUserIdAsync(Guid userId);
}