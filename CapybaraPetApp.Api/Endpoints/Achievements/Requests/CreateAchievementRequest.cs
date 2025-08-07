using CapybaraPetApp.Domain.AchievementAggregate;

namespace CapybaraPetApp.Api.Endpoints.Achievements.Requests;

public record CreateAchievementRequest(string Title, string Description, int Points, Rarity Rarity);