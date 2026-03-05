using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.LoginUser;

public record LoginUserQuery(string EmailOrUsername, string Password) : IQuery<ErrorOr<TokenResponseDto>>;