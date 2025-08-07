using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public class BuyItemCommandHandler : ICommandHandler<BuyItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;

    public BuyItemCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(BuyItemCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return UserErrors.NotFound;
        }

        var result = user.BuyItem(command.ItemId);
        await _userRepository.UpdateAsync(user);
        return result;
    }
}
