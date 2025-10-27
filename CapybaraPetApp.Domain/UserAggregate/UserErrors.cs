using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate;

public static class UserErrors
{
    public static Error NotFound = Error.NotFound(
        $"{nameof(User)}.{nameof(NotFound)}",
        "User was not found.");

    public static Error EmailAlreadyInUse = Error.Conflict(
        $"{nameof(User)}.{nameof(EmailAlreadyInUse)}",
        "Email is already in use.");

    public static Error CapybaraAlreadyAdopted = Error.Conflict(
        $"{nameof(User)}.{nameof(CapybaraAlreadyAdopted)}",
        "Capybara is already added to this user.");

    public static Error AchievementAlreadyAssigned = Error.Conflict(
        $"{nameof(User)}.{nameof(AchievementAlreadyAssigned)}",
        "Achievement has already been assigned to this user.");

    public static Error ItemNotOwned = Error.Conflict(
        $"{nameof(User)}.{nameof(ItemNotOwned)}",
        "User does not own the specified item.");

    public static Error InvalidPassword = Error.Validation(
        $"{nameof(User)}.{nameof(InvalidPassword)}",
        "Invalid password provided.");
}