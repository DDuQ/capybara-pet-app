using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public record CreateItemCommand(string Name, int Quantity, ItemDetail ItemDetail) : ICommand<ErrorOr<Item>>;