using System.ComponentModel.DataAnnotations;

namespace SmartGarage.DTOs;

public class UserUpdateDTO
{

    [StringLength(20, MinimumLength = 2)]
    public string? Username { get; set; }


    [StringLength(100)]
    public string? Password { get; set; } 


    [StringLength(250)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [StringLength(10)]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string? PhoneNumber { get; set; }
}