using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public record GetItemQuery(Guid ItemId, Guid UserId) : IQuery<ErrorOr<Item>>;
