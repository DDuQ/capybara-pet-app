namespace CapybaraPetApp.Api.Endpoints.Users.Requests;

public record RegisterUserRequest(string Username, string Email, Guid? Id);