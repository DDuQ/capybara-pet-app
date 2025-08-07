using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetItems;

public record GetItemsQuery(Guid UserId) : IQuery<ErrorOr<List<UserItem>>>;