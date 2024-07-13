using AutoMapper;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Application.Dtos;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public class GetCapybarasQueryHandler : IRequestHandler<GetCapybarasQuery, ErrorOr<List<CapybaraDto>>>
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

    public async Task<ErrorOr<List<CapybaraDto>>> Handle(GetCapybarasQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found.");
        }

        var capybaras = await _capybarasRepository.GetCapybarasByUserIdAsync(request.UserId);

        if (capybaras is null)
        {
            return Error.NotFound(description: "User does not have any capy-friends yet. ):");
        }

        return _mapper.Map<List<CapybaraDto>>(capybaras);
    }
}
