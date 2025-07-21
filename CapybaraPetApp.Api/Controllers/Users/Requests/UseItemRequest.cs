namespace CapybaraPetApp.Api.Controllers.Users.Requests;

public record UseItemRequest(Guid ItemId, Guid CapybaraId, int ItemQuantity);
