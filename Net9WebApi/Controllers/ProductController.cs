using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Bu controller sadece giris yapmis kullanicilara acik
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IReviewService _reviewService;
        
        // Dependency Injection ile servisi aliyoruz
        public ProductController(IProductService service, IReviewService reviewService)
        {
            _service = service;
            _reviewService = reviewService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<List<ProductDto>>.SuccessResponse(data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound(ApiResponse<ProductDto>.FailResponse("Product not found"));

            return Ok(ApiResponse<ProductDto>.SuccessResponse(data));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<ProductDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, ApiResponse<ProductDto>.SuccessResponse(data, "Product created successfully"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("Product not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Product updated successfully"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("Product not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(typeof(ApiResponse<List<ReviewDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviews(int id)
        {
            var data = await _reviewService.GetByProductIdAsync(id);
            return Ok(ApiResponse<List<ReviewDto>>.SuccessResponse(data));
        }
    }
}
