namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class InventoryItemDto
{
    public Guid ItemId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public string ItemDescription { get; init; } = string.Empty;
    public int Quantity { get; init; }
}