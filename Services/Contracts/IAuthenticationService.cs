namespace SmartGarage.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateUserAsync(string usernameOrEmail, string password, HttpContext httpContext);
        Task SignOutAsync(HttpContext httpContext);
    }
}