using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public class CreateCapybaraCommandHandler : ICommandHandler<CreateCapybaraCommand, ErrorOr<Guid>>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CreateCapybaraCommandHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateCapybaraCommand command, CancellationToken cancellationToken)
    {
        //if (command.Id is not null)
        //{
        //    capybara = await _capybaraRepository.GetByIdAsync((Guid)command.Id);
        //}

        //if (capybara is not null)
        //{
        //    return Error.NotFound(description: "Capybara not found.");
        //}

        var capybara = new Capybara(command.Name, command.Stats);

        await _capybaraRepository.AddAsync(capybara);

        return capybara.Id;
    }
}
