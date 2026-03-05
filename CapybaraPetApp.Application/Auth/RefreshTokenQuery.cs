using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using ErrorOr;

namespace CapybaraPetApp.Application.Auth;

public record RefreshTokenQuery(Guid UserId, string RefreshToken) : IQuery<ErrorOr<TokenResponseDto>>, IQuery<TokenResponseDto>;