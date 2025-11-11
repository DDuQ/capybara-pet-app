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
        public const string AdoptCapybara = $"{Base}/{{id:guid}}/capybaras";
        public const string UnlockAchievement = $"{Base}/{{id:guid}}/achievements";
        public const string GetItems = $"{Base}/{{id:guid}}/items";
        public const string BuyItem = $"{Base}/{{id:guid}}/purchases";
        public const string UseItem = $"{Base}/{{id:guid}}/items";
    }

    public static class Item
    {
        private const string Base = $"{BaseUrl}/items";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
    }
}