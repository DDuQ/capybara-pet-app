using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetItems;

public record GetUserItemsQuery(Guid UserId) : IQuery<ErrorOr<List<InventoryItemDto>>>;