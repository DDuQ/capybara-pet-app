using AutoMapper;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public class GetCapybarasQueryHandler : IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>>
{
    private readonly ICapybaraRepository _capybarasRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetCapybarasQueryHandler(ICapybaraRepository capybarasRepository, IUserRepository userRepository, IMapper mapper)
    {
        _capybarasRepository = capybarasRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<Capybara>>> Handle(GetCapybarasQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found."); //TODO: Add error code to Domain (UserErrors).
        }

        var capybaras = await _capybarasRepository.GetCapybarasByUserIdAsync(query.UserId);

        if (capybaras is null)
        {
            return Error.NotFound(description: "User does not have any capy-friends yet. ):"); //TODO: Add error code to Domain (UserErrors).
        }

        return capybaras;
    }
}
