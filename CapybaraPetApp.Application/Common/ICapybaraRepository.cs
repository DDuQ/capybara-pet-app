using CapybaraPetApp.Domain.CapybaraAggregate;

namespace CapybaraPetApp.Application.Common;

public interface ICapybaraRepository
{
    Task AddAsync(Capybara capybara);

    Task<Capybara?> GetByIdAsync(Guid id);

    Task UpdateAsync(Capybara entity);

    Task<List<Capybara>> GetCapybarasByUserIdAsync(Guid userId);
}