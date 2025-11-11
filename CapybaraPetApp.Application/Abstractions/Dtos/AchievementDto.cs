namespace CapybaraPetApp.Application.Abstractions.Dtos;

public class AchievementDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public DateTime CreatedAt { get; set; }
}