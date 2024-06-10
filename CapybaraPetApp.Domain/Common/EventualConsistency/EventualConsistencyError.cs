using ErrorOr;

namespace CapybaraPetApp.Domain.Common.EventualConsistency;

public class EventualConsistencyError
{
    public const int EventualConsistencyType = 100;

    public static Error From(string code, string description)
    {
        return Error.Custom(EventualConsistencyType, code, description);
    }
}
