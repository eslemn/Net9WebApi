using Net9WebApi.DTOs.Category;

namespace Net9WebApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto);
    }
}

