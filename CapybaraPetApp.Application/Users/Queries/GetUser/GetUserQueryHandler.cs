﻿using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.Id);

        if (user is null)
        {
            return Error.NotFound(description: "User not found.");
        }

        return user;
    }
}
