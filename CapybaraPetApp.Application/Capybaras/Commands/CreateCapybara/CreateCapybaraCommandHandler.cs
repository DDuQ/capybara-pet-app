using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public class CreateCapybaraCommandHandler : ICommandHandler<CreateCapybaraCommand, ErrorOr<Capybara>>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CreateCapybaraCommandHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Capybara>> Handle(CreateCapybaraCommand command, CancellationToken cancellationToken)
    {
        var capybara = new Capybara(command.Name);

        await _capybaraRepository.AddAsync(capybara);

        return capybara;
    }
}