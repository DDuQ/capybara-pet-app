using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public class CreateInteractionCommandHandler : ICommandHandler<CreateInteractionCommand, ErrorOr<Interaction>>
{
    public Task<ErrorOr<Interaction>> Handle(CreateInteractionCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
