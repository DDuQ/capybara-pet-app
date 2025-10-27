using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public record GetItemQuery(Guid ItemId) : IQuery<ErrorOr<Item>>;