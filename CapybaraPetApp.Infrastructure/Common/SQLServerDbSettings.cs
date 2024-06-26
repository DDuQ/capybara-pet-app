namespace CapybaraPetApp.Infrastructure.Common;

public class SQLServerDbSettings
{
    public const string Section = "SQLServerDbSettings";
    public string ConnectionString { get; set; }
}