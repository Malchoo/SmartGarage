using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Models;
using SmartGarage.Repostiories.Contracts;

namespace SmartGarage.Repostiories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRepository(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByPhoneAsync(string phone)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task<Customer?> GetUserByVehicleLicensePlateAsync(string licensePlate)
    {
        return await _context.Customers
            .OfType<Customer>()
            .Where(c => c.Vehicles.Any(v => v.LicensePlate == licensePlate))
            .FirstOrDefaultAsync();
    }

    public async Task<Customer?> GetUserByVehicleVINAsync(string vin)
    {
        return await _context.Customers
            .OfType<Customer>()
            .Where(c => c.Vehicles.Any(v => v.VIN == vin))
            .FirstOrDefaultAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Customers.FindAsync(customerId);
    }

    public async Task<User> AddUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> AuthenticateAsync(string usernameOrEmail, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u =>
            u.Username == usernameOrEmail || u.Email == usernameOrEmail);

        if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
        {
            return user;
        }

        return null;
    }
    public async Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        if (_passwordHasher.VerifyHashedPassword(user, user.Password, currentPassword) == PasswordVerificationResult.Success)
        {
            user.Password = _passwordHasher.HashPassword(user, newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return false;

        user.PasswordResetToken = Guid.NewGuid().ToString();
        user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(1);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
        if (user == null || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            return false;

        user.Password = _passwordHasher.HashPassword(user, newPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiration = null;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> GetUserByPasswordResetTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
    }
}