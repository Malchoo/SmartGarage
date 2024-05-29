using System.ComponentModel.DataAnnotations;

namespace SmartGarage.DTOs;

public class UserCreationDTO
{
    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [Required]
    [StringLength(250)]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required]
    [StringLength(10)]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")] // 10 digits phone number
    public string PhoneNumber { get; set; }
}
