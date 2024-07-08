using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public class CreateInteractionCommandHandler : IRequestHandler<CreateInteractionCommand, ErrorOr<Interaction>>
{
    public Task<ErrorOr<Interaction>> Handle(CreateInteractionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
