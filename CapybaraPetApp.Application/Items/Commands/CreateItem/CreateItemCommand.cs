using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public record CreateItemCommand(string Name, ItemDetail ItemDetail) : ICommand<ErrorOr<Item>>;