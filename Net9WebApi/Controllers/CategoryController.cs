using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs.Category;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(ApiResponse<List<CategoryResponseDto>>.SuccessResponse(
                categories,
                "Categories listed successfully"
            ));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAll),
                ApiResponse<CategoryResponseDto>.SuccessResponse(
                    result,
                    "Category created successfully"
                ));
        }
    }
}
