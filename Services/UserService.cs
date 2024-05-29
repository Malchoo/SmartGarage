using Microsoft.AspNetCore.Identity;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    //private readonly IEmailService _emailService;

    public UserService(
        IUserRepository userRepository, 
        IPasswordHasher<User> passwordHasher) 
        //IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
       // _emailService = emailService;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _userRepository.GetUserByIdAsync(userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<User?> GetUserByPhoneAsync(string phone)
    {
        return await _userRepository.GetUserByPhoneAsync(phone);
    }

    public async Task<Customer?> GetUserByVehicleLicensePlateAsync(string licensePlate)
    {
        return await _userRepository.GetUserByVehicleLicensePlateAsync(licensePlate);
    }

    public async Task<Customer?> GetUserByVehicleVINAsync(string vin)
    {
        return await _userRepository.GetUserByVehicleVINAsync(vin);
    }

    public async Task<User> AddUserAsync(User user)
    {
        string hashedPassword = _passwordHasher.HashPassword(user, user.Password);
        user.Password = hashedPassword;
        return await _userRepository.AddUserAsync(user);
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        return await _userRepository.DeleteUserAsync(userId);
    }

    public async Task<User> AuthenticateAsync(string usernameOrEmail, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(usernameOrEmail);
        if (user == null)
            user = await _userRepository.GetUserByEmailAsync(usernameOrEmail);

        if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
        {
            return user;
        }

        return null;
    }

    public async Task<bool> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
            return false;

        user.PasswordResetToken = Guid.NewGuid().ToString();
        user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(1);
        await _userRepository.UpdateUserAsync(user);

        // Send password reset email using the IEmailService
        string resetUrl = $"https://smartgarage.com/reset-password?token={user.PasswordResetToken}";
        string emailBody = $"Please click the following link to reset your password: {resetUrl}";
        //await _emailService.SendEmailAsync(user.Email, "Reset Your Password", emailBody);

        return true;
    }

    public async Task<bool> ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _userRepository.GetUserByPasswordResetTokenAsync(token);
        if (user == null || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            return false;

        string hashedPassword = _passwordHasher.HashPassword(user, newPassword);
        user.Password = hashedPassword;
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiration = null;
        await _userRepository.UpdateUserAsync(user);

        return true;
    }
}