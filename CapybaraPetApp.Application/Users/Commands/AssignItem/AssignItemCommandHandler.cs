using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public class AssignItemCommandHandler : ICommandHandler<AssignItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;

    public AssignItemCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignItemCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "Item does not exists."); //TODO: Add error code to Domain (ItemErrors).
        }

        var result = user.BuyItem(command.ItemId);
        await _userRepository.UpdateAsync(user);
        return result;
    }
}
