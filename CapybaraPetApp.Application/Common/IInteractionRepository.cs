using CapybaraPetApp.Domain.InteractionAggregate;

namespace CapybaraPetApp.Application.Common;

public interface IInteractionRepository
{
    Task AddAsync(Interaction interaction);

    Task<Interaction?> GetByIdAsync(Guid id);

    Task UpdateAsync(Interaction entity);
}