using SmartGarage.Models;

namespace SmartGarage.Repostiories.Contracts;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneAsync(string phone);
    Task<Customer?> GetUserByVehicleLicensePlateAsync(string licensePlate);
    Task<Customer?> GetUserByVehicleVINAsync(string vin);
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    Task<User> AuthenticateAsync(string usernameOrEmail, string password);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByPasswordResetTokenAsync(string token);
    Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<bool> GeneratePasswordResetTokenAsync(string email);
    Task<bool> ResetPasswordAsync(string token, string newPassword);
}