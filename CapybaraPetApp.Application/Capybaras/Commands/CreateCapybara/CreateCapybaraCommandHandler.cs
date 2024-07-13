using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public class CreateCapybaraCommandHandler : IRequestHandler<CreateCapybaraCommand, ErrorOr<Capybara>>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CreateCapybaraCommandHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Capybara>> Handle(CreateCapybaraCommand request, CancellationToken cancellationToken)
    {
        Capybara? capybara = null;

        if (request.Id is not null)
        {
            capybara = await _capybaraRepository.GetByIdAsync((Guid)request.Id);
        }

        if (capybara is not null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        capybara = new Capybara(
            request.Name,
            request.Stats,
            request.Id);

        await _capybaraRepository.AddAsync(capybara);

        return capybara;
    }
}
