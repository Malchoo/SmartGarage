using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool IsDeleted { get; set; }
    public bool IsEmployee { get; set; }

    [Required]
    [StringLength(20, MinimumLength = 2)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)] // Assuming hashed password storage, can be longer
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


    [StringLength(500)]
    public string PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiration { get; set; }

}
