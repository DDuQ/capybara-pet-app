using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler(IItemRepository itemRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateItemCommand, ErrorOr<Item>>
{
    public async Task<ErrorOr<Item>> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        if (await itemRepository.ExistsByNameAsync(command.Name)) return ItemErrors.ItemAlreadyExists;

        var item = new Item(command.Name, command.ItemDetail);

        await itemRepository.AddAsync(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return item;
    }
}