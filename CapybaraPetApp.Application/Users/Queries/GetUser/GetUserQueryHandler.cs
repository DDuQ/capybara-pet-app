using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user is null) 
        {
            return Error.NotFound(description: "User not found.");
        }

        return user;
    }
}
