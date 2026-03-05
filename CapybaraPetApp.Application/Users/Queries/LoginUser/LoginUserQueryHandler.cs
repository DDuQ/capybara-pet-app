using System.Text.RegularExpressions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Application.Auth;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CapybaraPetApp.Application.Users.Queries.LoginUser;

public partial class LoginUserQueryHandler(IUserRepository userRepository, IAuthService authService) : IQueryHandler<LoginUserQuery, ErrorOr<TokenResponseDto>>
{
    public async Task<ErrorOr<TokenResponseDto>> Handle(LoginUserQuery query, CancellationToken cancellationToken = default)
    {
        var isEmail = EmailRegex().IsMatch(query.EmailOrUsername);
        User? user;
        
        if (isEmail)
        {
            user = await userRepository.GetByEmailAsync(query.EmailOrUsername);                           
        }
        
        user = await userRepository.GetByUsernameAsync(query.EmailOrUsername);
        
        if (user is null) return UserErrors.NotFound;
        
        var isPasswordValid = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, query.Password) 
                              == PasswordVerificationResult.Success;
        
        if (!isPasswordValid) return UserErrors.InvalidPassword;
        
        var accessToken = authService.GenerateAccessToken(user);
        var authToken = await authService.GenerateAndSaveRefreshToken(user.Id);

         return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = authToken.RefreshToken
        };
    }

    [GeneratedRegex(@"^[^@]+@[^@]+\.[^@]+$")]
    private static partial Regex EmailRegex();
}