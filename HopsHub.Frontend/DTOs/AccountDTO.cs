namespace HopsHub.Shared.DTOs;

public class LoginDTO
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class CreateUserDTO
{
    public required string Email { get; set; }

    public required string Password { get; set; }
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