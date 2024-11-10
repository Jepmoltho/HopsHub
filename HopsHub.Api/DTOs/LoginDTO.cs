namespace HopsHub.Api.DTOs;

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

