namespace CapybaraPetApp.Api.Endpoints.Users.Requests;

public record UseItemRequest(Guid ItemId, Guid CapybaraId, int ItemAmount);