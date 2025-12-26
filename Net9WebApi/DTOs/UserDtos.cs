namespace Net9WebApi.DTOs;

public record CreateUserDto(string FirstName, string LastName, string Username, string Email, string Password);
public record UpdateUserDto(string FirstName, string LastName, string Username, string Email); // Password change usually separate
public record UserDto(int Id, string FirstName, string LastName, string Username, string Email, DateTime CreatedAt);
public record LoginDto(string Username, string Password);
