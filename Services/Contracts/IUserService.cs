using SmartGarage.Models;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneAsync(string phone);
    Task<Customer?> GetUserByVehicleLicensePlateAsync(string licensePlate);
    Task<Customer?> GetUserByVehicleVINAsync(string vin);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(int userId);
    Task<User> AuthenticateAsync(string usernameOrEmail, string password);
    Task<bool> GeneratePasswordResetTokenAsync(string email);
    Task<bool> ResetPasswordAsync(string token, string newPassword);
}