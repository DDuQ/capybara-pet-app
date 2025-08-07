using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Queries.GetItem;

public record GetItemQuery(Guid ItemId) : IQuery<ErrorOr<Item>>;
