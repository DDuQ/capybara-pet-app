using CapybaraPetApp.Application.Abstractions;
using ErrorOr;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public class CreateInteractionCommandHandler : ICommandHandler<CreateInteractionCommand, ErrorOr<Guid>>
{
    public Task<ErrorOr<Guid>> Handle(CreateInteractionCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(); //TODO: Implement this method to handle the creation of an interaction.
    }
}
