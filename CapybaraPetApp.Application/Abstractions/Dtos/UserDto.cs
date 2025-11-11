namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class UserDto
{
    public IList<CapybaraDto>? Capybaras { get; set; }
    public IList<ItemDto>? Items { get; set; }
    public IList<AchievementDto>? Achievements { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid Id { get; set; }
}