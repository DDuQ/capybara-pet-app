using CapybaraPetApp.Application.Dtos;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public record GetCapybarasQuery(
    Guid UserId) : IRequest<ErrorOr<List<CapybaraDto>>>;