namespace CapybaraPetApp.Application.Dtos;

public class UserDto
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IReadOnlyCollection<UserAchievementDto> UserAchievements;
    public IReadOnlyCollection<CapybaraDto> Capybaras;
    public IReadOnlyCollection<InteractionDto> Interactions;
    public IReadOnlyCollection<ItemDto> Items;
}
