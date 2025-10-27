namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class ItemDto
{
    public string Name { get; set; }
    public int Amount { get; set; } = 0;
    public ItemDetailDto ItemDetail { get; set; }
}