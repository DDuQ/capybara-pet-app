using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Application.Abstractions.Dtos;

public record ItemDetailDto
{
    public ItemType ItemType { get; set; }
    public int BonusEffect { get; set; }
    public int Quantity { get; set; }
}