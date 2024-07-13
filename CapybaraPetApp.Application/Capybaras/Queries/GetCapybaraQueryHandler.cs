using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Queries;

public class GetCapybaraQueryHandler : IRequestHandler<GetCapybaraQuery, ErrorOr<Capybara>>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public GetCapybaraQueryHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Capybara>> Handle(GetCapybaraQuery request, CancellationToken cancellationToken)
    {
        var capybara = await _capybaraRepository.GetByIdAsync(request.CapybaraId);
        
        if (capybara is null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        return capybara;
    }
}
