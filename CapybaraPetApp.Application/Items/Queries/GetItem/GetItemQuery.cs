using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public record GetItemQuery(Guid ItemId, Guid UserId) : IRequest<ErrorOr<Item>>;
