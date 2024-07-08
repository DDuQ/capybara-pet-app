using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Items.CreateItem;

public record CreateItemCommand(string Name, ItemDetail ItemDetail) : IRequest<ErrorOr<Item>>;