using CapybaraPetApp.Application.Abstractions.Clients;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUserRepository userRepository, IAzureBlobClient capybuddyBlobClient) : IQueryHandler<GetUserQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAllRelatedDataByIdAsync(query.Id);

        if (user is null) return UserErrors.NotFound;

        //TODO: Define a way to either use default profile picture or user's profile picture.
        user.ProfilePictureUrl = capybuddyBlobClient.GetUserProfilePicture(user.Id, true);

        return user;
    }
}