using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Dtos;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public record GetCapybarasQuery(
    Guid UserId) : IQuery<ErrorOr<List<CapybaraDto>>>;