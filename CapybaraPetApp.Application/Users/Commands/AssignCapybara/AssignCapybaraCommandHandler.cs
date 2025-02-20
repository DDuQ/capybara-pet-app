﻿using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddCapybara;

public class AssignCapybaraCommandHandler : IRequestHandler<AssignCapybaraCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public AssignCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignCapybaraCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return Error.NotFound(description: "User not found.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(request.CapybaraId);

        if (capybara == null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        user.AssignCapybara(capybara);

        await _userRepository.UpdateAsync(user);
        
        return Result.Success;
    }
}
