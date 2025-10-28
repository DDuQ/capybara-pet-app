using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public class CreateCapybaraCommandHandler(ICapybaraRepository capybaraRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCapybaraCommand, ErrorOr<Capybara>>
{
    public async Task<ErrorOr<Capybara>> Handle(CreateCapybaraCommand command, CancellationToken cancellationToken)
    {
        var capybara = new Capybara(command.Name);

        await capybaraRepository.AddAsync(capybara);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return capybara;
    }
}