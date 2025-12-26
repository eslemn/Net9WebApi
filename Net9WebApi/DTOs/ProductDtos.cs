namespace Net9WebApi.DTOs;

public record CreateProductDto(string Name, string? Description, decimal Price, int Stock, int CategoryId);
public record UpdateProductDto(string Name, string? Description, decimal Price, int Stock, int CategoryId);
public record ProductDto(int Id, string Name, string? Description, decimal Price, int Stock, int CategoryId, string CategoryName, DateTime CreatedAt);
