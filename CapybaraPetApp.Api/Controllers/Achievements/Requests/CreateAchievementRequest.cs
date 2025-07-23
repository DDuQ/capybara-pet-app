using CapybaraPetApp.Domain.AchievementAggregate;

namespace CapybaraPetApp.Api.Controllers.Achievements.Requests;

public record CreateAchievementRequest(string Title, string Description, int Points, Rarity Rarity);