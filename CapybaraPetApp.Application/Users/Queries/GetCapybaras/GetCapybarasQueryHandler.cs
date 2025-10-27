using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public class GetCapybarasQueryHandler : IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>>
{
    private readonly ICapybaraRepository _capybarasRepository;
    private readonly IUserRepository _userRepository;

    public GetCapybarasQueryHandler(ICapybaraRepository capybarasRepository, IUserRepository userRepository)
    {
        _capybarasRepository = capybarasRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<Capybara>>> Handle(GetCapybarasQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user is null) return UserErrors.NotFound;

        var capybaras = await _capybarasRepository.GetCapybarasByUserIdAsync(query.UserId);

        return capybaras;
    }
}