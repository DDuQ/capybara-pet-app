namespace CapybaraPetApp.Api;

public static class APIEndpoints
{
    private const string BaseUrl = "api";

    public static class Achievements
    {
        private const string Base = $"{BaseUrl}/achievements";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
    }

    public static class Capybara
    {
        private const string Base = $"{BaseUrl}/capybaras";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
    }

    public static class User
    {
        private const string Base = $"{BaseUrl}/user";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetCapybaras = $"{Base}/{{id:guid}}/capybaras";
        public const string AdoptCapybara = $"{Base}/{{userId:guid}}/capybaras";
        public const string UnlockAchievement = $"{Base}/{{userId:guid}}/achievements/{{achievementId:guid}}";
        public const string GetItems = $"{Base}/{{userId:guid}}/items";
        public const string BuyItem = $"{Base}/{{userId:guid}}/purchases";
        public const string UseItem = $"{Base}/{{userId:guid}}/items/{{itemId:guid}}";
    }

    public static class Item
    {
        private const string Base = $"{BaseUrl}/items";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
    }
}