using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddInteraction;

public class AddInteractionCommandHandler : IRequestHandler<AddInteractionCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public AddInteractionCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AddInteractionCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null) 
        {
            return Error.NotFound(description: "User does not exists.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(request.CapybaraId);

        if (capybara is null)
        {
            return Error.NotFound(description:"Capybara does not exists.");
        }

        if (!user.Owns(capybara))
        {
            return Error.Conflict(description: "Capybara is not owned by this user.");
        }

        var interactionDetailResult = InteractionDetail.IsInteractionDetailValid(request.InteractionDetail);

        if (interactionDetailResult.IsError)
        {
            return interactionDetailResult.Errors;
        }
        
        var interaction = new Interaction(request.InteractionDetail, request.UserId, request.CapybaraId);

        user.AddInteraction(interaction);
        
        await _userRepository.UpdateAsync(user);

        return Result.Success;
    }
}
