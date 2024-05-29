using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;
using System.Security.Claims;

namespace SmartGarage.Services;

public class AuthenticationService : Contracts.IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> AuthenticateUserAsync(string usernameOrEmail, string password, HttpContext httpContext)
    {
        var user = await _userRepository.AuthenticateAsync(usernameOrEmail, password);

        if (user == null)
            return false;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user is Customer ? "Customer" : "Employee")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return true;
    }

    public async Task SignOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}

//public interface IAuthenticationService
//{
//    Task<bool> AuthenticateUserAsync(string usernameOrEmail, string password, HttpContext httpContext);
//    Task SignOutAsync(HttpContext httpContext);
//}