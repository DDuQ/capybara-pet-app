﻿using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddItem;

public class AssignItemCommandHandler : IRequestHandler<AssignItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;

    public AssignItemCommandHandler(IUserRepository userRepository, IItemRepository itemRepository)
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item does not exists.");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "Item does not exists.");
        }

        user.AssignItem(item);

        return Result.Success;
    }
}
