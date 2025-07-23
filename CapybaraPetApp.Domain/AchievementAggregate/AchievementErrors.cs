using ErrorOr;

namespace CapybaraPetApp.Domain.AchievementAggregate;

public static class AchievementErrors
{
    public static Error NotFound => Error.NotFound(
        $"{nameof(Achievement)}.{nameof(NotFound)}",
        "Achievement was not found.");

    public static Error AlreadyUnlocked => Error.Conflict(
        $"{nameof(Achievement)}.{nameof(AlreadyUnlocked)}",
        "Achievement has already been unlocked by this user.");

    public static Error InvalidRarity => Error.Validation(
        $"{nameof(Achievement)}.{nameof(InvalidRarity)}",
        "Achievement rarity is invalid.");
        
    public static Error InvalidAchievementType => Error.Validation(
        $"{nameof(Achievement)}.{nameof(InvalidAchievementType)}",
        "Achievement type is invalid.");

    public static Error InvalidPoints => Error.Validation(
        $"{nameof(Achievement)}.{nameof(InvalidPoints)}",
        "Achievement points must be a positive number.");
        
    public static Error InvalidTitle => Error.Validation(
        $"{nameof(Achievement)}.{nameof(InvalidTitle)}",
        "Achievement title cannot be empty.");
        
    public static Error InvalidDescription => Error.Validation(
        $"{nameof(Achievement)}.{nameof(InvalidDescription)}",
        "Achievement description cannot be empty.");
        
    public static Error AchievementNotUnlocked(Guid achievementId) => Error.Conflict(
        $"{nameof(Achievement)}.{nameof(AchievementNotUnlocked)}",
        $"Achievement with ID '{achievementId}' has not been unlocked yet.");
}
