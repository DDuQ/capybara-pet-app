namespace CapybaraPetApp.Api.Endpoints.Users.Requests;

public record LoginUserRequest(string UsernameOrEmail, string Password);