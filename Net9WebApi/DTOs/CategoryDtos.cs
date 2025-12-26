namespace Net9WebApi.DTOs;

public record CreateCategoryDto(string Name, string? Description);
public record UpdateCategoryDto(string Name, string? Description);
public record CategoryDto(int Id, string Name, string? Description, DateTime CreatedAt);
