using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public static class UserErrors
{
    public static Error NotFound { get; } = Error.NotFound(
        $"{nameof(User)}.{nameof(NotFound)}",
        "User was not found.");

    public static Error EmailAlreadyInUse { get; } = Error.Conflict(
        $"{nameof(User)}.{nameof(EmailAlreadyInUse)}",
        "Email is already in use.");

    public static Error CapybaraAlreadyAdopted { get; } = Error.Conflict(
        $"{nameof(User)}.{nameof(CapybaraAlreadyAdopted)}",
        "Capybara is already added to this user.");

    public static Error AchievementAlreadyAssigned { get; } = Error.Conflict(
        $"{nameof(User)}.{nameof(AchievementAlreadyAssigned)}",
        "Achievement has already been assigned to this user.");

    public static Error ItemNotOwned { get; } = Error.Conflict(
        $"{nameof(User)}.{nameof(ItemNotOwned)}",
        "User does not own the specified item.");

    public static Error InvalidPassword { get; } = Error.Validation(
        $"{nameof(User)}.{nameof(InvalidPassword)}",
        "Invalid password provided.");
}