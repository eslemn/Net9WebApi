using Net9WebApi.DTOs;

namespace Net9WebApi.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllAsync();
        Task<ReviewDto?> GetByIdAsync(int id);
        Task<ReviewDto> CreateAsync(CreateReviewDto dto);
        Task<bool> UpdateAsync(int id, UpdateReviewDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ReviewDto>> GetByProductIdAsync(int productId);
        Task<List<ReviewDto>> GetByUserIdAsync(int userId);
    }
}
