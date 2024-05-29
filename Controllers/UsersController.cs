using Microsoft.AspNetCore.Mvc;
using SmartGarage.DTOs;
using SmartGarage.Models;

namespace SmartGarage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreationDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = new User()
        {
            IsEmployee = false,
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordResetToken = ""
        };

        var createdUser = await _userService.AddUserAsync(result);
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userResult = _userService.GetUserByIdAsync(id).Result;
        if (userResult is null)
        {
            return BadRequest();
        }

        userResult.Username = dto.Username ?? userResult.Username;
        userResult.Password = dto.Password ?? userResult.Password;
        userResult.Email = dto.Email ?? userResult.Email;
        userResult.PhoneNumber = dto.PhoneNumber ?? userResult.PhoneNumber;

        try
        {
            var updatedUser = await _userService.UpdateUserAsync(userResult);
            return Ok(updatedUser);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);
        if (!result)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user.");
        }

        return StatusCode(StatusCodes.Status200OK, "This user was deleted successfuly.");
    }

    [HttpGet("username/{username}")]
    public async Task<IActionResult> SearchUsersByUsername(string username)
    {
        var user = await _userService.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> SearchUsersByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("phone/{phone}")]
    public async Task<IActionResult> SearchUsersByPhone(string phone)
    {
        var user = await _userService.GetUserByPhoneAsync(phone);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("licensePlate/{licensePlate}")]
    public async Task<IActionResult> SearchUsersByLicensePlate(string licensePlate)
    {
        var user = await _userService.GetUserByVehicleLicensePlateAsync(licensePlate);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("vin/{vin}")]
    public async Task<IActionResult> SearchUsersByVIN(string vin)
    {
        var user = await _userService.GetUserByVehicleVINAsync(vin);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(string email)
    {
        var result = await _userService.GeneratePasswordResetTokenAsync(email);
        if (result)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("resetPassword/{token}")]
    public async Task<IActionResult> ResetPassword(string token, [FromBody] string newPassword)
    {
        var result = await _userService.ResetPasswordAsync(token, newPassword);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest("Invalid token or token expired.");
        }
    }
}