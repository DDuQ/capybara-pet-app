namespace CapybaraPetApp.Api.Controllers.Users.Requests;

public record RegisterUserRequest(string Username, string Email, Guid Id);
