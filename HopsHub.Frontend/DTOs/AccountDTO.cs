using System.ComponentModel.DataAnnotations;

namespace HopsHub.Shared.DTOs;
//To do: Publish shared models as nuget package 

public class LoginDTO
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

//Differs from backend model
public class CreateUserDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;
}

public class DeleteUserDTO
{
    public required Guid Id { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class PasswordResetRequestDTO
{
    public required string Email { get; set; }
}

public class ResetPasswordDTO
{
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required string NewPassword { get; set; }
}
