namespace CapybaraPetApp.Api.Endpoints.Users.Requests;

public record UseItemRequest(Guid UserId, Guid ItemId, Guid CapybaraId, int ItemAmount);