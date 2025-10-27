using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Queries;

public class GetCapybaraQueryHandler : IQueryHandler<GetCapybaraQuery, ErrorOr<Capybara>>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public GetCapybaraQueryHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Capybara>> Handle(GetCapybaraQuery query, CancellationToken cancellationToken)
    {
        var capybara = await _capybaraRepository.GetByIdAsync(query.CapybaraId);

        if (capybara is null) return CapybaraErrors.NotFound;

        return capybara;
    }
}