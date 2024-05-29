using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.Repostiories.Contracts;
using System.Security.Claims;

namespace SmartGarage.Controllers;

public class AccountController : Controller
{
    private readonly Services.Contracts.IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;

    public AccountController(
        Services.Contracts.IAuthenticationService authenticationService,
        IUserRepository userRepository)
    {
        _authenticationService = authenticationService;
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string usernameOrEmail, string password)
    {
        var user = await _userRepository.AuthenticateAsync(usernameOrEmail, password);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
            return View();
        }

        var claims = new List<Claim>
    {
    new Claim(ClaimTypes.Name, user.Username),
    new Claim(ClaimTypes.Email, user.Email),
    };

        if (!user.IsEmployee)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Customer"));
        }
        else if (user.IsEmployee)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Employee"));
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _authenticationService.SignOutAsync(HttpContext);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userRepository.GetUserByIdAsync(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            if (user != null)
            {
                var result = await _userRepository.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to change password.");
                }
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userRepository.GeneratePasswordResetTokenAsync(model.Email);
            if (result)
            {
                // Send password reset email
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to generate password reset token.");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid password reset token.");
        }
        return View(new ResetPasswordViewModel { Token = token });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userRepository.ResetPasswordAsync(model.Token, model.Password);
            if (result)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to reset password.");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}