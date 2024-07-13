namespace CapybaraPetApp.Application.Dtos;

public class UserAchievementDto
{
    public Guid UserId { get; set; }
    public Guid AchievementId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}