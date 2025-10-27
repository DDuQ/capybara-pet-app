using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public static class CapybaraErrors
{
    public static Error NotFound => Error.NotFound(
        $"{nameof(Capybara)}.{nameof(NotFound)}",
        "Capybara was not found.");

    public static Error NameRequired => Error.Validation(
        $"{nameof(Capybara)}.{nameof(NameRequired)}",
        "Name is required.");

    public static Error AlreadyAssigned => Error.Conflict(
        $"{nameof(Capybara)}.{nameof(AlreadyAssigned)}",
        "Capybara is already assigned to a user.");

    public static Error InvalidStats => Error.Validation(
        $"{nameof(Capybara)}.{nameof(InvalidStats)}",
        "Capybara stats are invalid.");

    public static Error InvalidHappiness => Error.Validation(
        $"{nameof(Capybara)}.{nameof(InvalidHappiness)}",
        "Happiness must be between 0 and 100.");

    public static Error InvalidHealth => Error.Validation(
        $"{nameof(Capybara)}.{nameof(InvalidHealth)}",
        "Health must be between 0 and 100.");

    public static Error InvalidEnergy => Error.Validation(
        $"{nameof(Capybara)}.{nameof(InvalidEnergy)}",
        "Energy must be between 0 and 100.");

    public static Error InvalidStatChange => Error.Validation(
        $"{nameof(Capybara)}.{nameof(InvalidStatChange)}",
        "Stat change amount must be greater than zero.");

    public static Error InsufficientEnergy(int currentEnergy)
    {
        return Error.Conflict(
            $"{nameof(Capybara)}.{nameof(InsufficientEnergy)}",
            $"Capy does not have enough energy (currently: {currentEnergy}) to perform this action.");
    }
}