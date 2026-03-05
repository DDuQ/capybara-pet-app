namespace CapybaraPetApp.Api.Endpoints.RefreshToken;

public record RefreshTokenRequest(Guid UserId, string RefreshToken);