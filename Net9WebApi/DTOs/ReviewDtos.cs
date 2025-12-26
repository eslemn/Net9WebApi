namespace Net9WebApi.DTOs;

public record CreateReviewDto(int Rating, string Comment, int UserId, int ProductId);
public record UpdateReviewDto(int Rating, string Comment);
public record ReviewDto(int Id, int Rating, string Comment, int UserId, string UserName, int ProductId, string ProductName, DateTime CreatedAt);
