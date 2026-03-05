namespace CapybaraPetApp.Application.Auth.Utils;

public class AuthToken
{
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
    public Guid UserId { get; set; }
}