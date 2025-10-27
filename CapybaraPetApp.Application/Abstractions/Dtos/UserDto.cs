namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class UserDto
{
    public IReadOnlyCollection<CapybaraDto> Capybaras;
    public IReadOnlyCollection<InteractionDto> Interactions;
    public IReadOnlyCollection<ItemDto> Items;
    public IReadOnlyCollection<UserAchievementDto> UserAchievements;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
}