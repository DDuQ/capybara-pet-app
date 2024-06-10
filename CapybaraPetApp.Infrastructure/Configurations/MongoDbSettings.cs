namespace CapybaraPetApp.Infrastructure.Configurations;

public class MongoDbSettings
{
    public const string Section = "MongoDbSettings";
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
