using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Application.Auth.Utils;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CapybaraPetApp.Application.Auth;

public class AuthService(IAuthTokenRepository authTokenRepository, IUnitOfWork unitOfWork,  IOptions<Jwt> jwt) : IAuthService
{
    private readonly Jwt _jwt = jwt.Value;

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public async Task<AuthToken> GenerateAndSaveRefreshToken(Guid userId)
    {
        var loginToken = new AuthToken()
        {
            UserId = userId
        };
        FillToken(loginToken);
        authTokenRepository.Add(loginToken);
        await unitOfWork.SaveChangesAsync();
        return loginToken;
    }

    public async Task<AuthToken?> IssueNewRefreshToken(Guid userId, string refreshToken)
    {
        var authToken = await authTokenRepository.GetByUserIdAsync(userId);

        if (authToken is null || !IsRefreshTokenValid(authToken, refreshToken)) return null;
        
        FillToken(authToken);
        authTokenRepository.Add(authToken);
        await unitOfWork.SaveChangesAsync();
        return authToken;
    }

    private static void FillToken(AuthToken token)
    {
        token.RefreshToken = GenerateRefreshToken();
        token.Expiration = DateTime.UtcNow.AddMinutes(5);
    }

    private static bool IsRefreshTokenValid(AuthToken token, string refreshToken)
    {
        return token.Expiration > DateTime.UtcNow && string.Equals(token.RefreshToken, refreshToken);
    }
    
    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwt.Key));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwt.Issuer,
            Audience = _jwt.Audience,
            Expires = DateTime.Now.AddMinutes(30),
            SigningCredentials = credentials
        };
        
        return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
    }
}