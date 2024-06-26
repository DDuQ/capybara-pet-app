using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybara;

public class GetCapybarasQueryHandler : IRequestHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>>
{
    private readonly ICapybaraRepository _capybarasRepository;

    public GetCapybarasQueryHandler(ICapybaraRepository capybarasRepository)
    {
        _capybarasRepository = capybarasRepository;
    }

    public async Task<ErrorOr<List<Capybara>>> Handle(GetCapybarasQuery request, CancellationToken cancellationToken)
    {
        var capybaras = await _capybarasRepository.GetCapybarasByUserIdAsync(request.UserId);

        if (capybaras == null)
        {
            return Error.NotFound("User does not have any capy-friends yet. ):");
        }

        return capybaras;
    }
}
