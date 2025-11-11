using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Api.Endpoints.Items.Requests;

public record CreateItemRequest(string Name, ItemType ItemType, int BonusEffect);