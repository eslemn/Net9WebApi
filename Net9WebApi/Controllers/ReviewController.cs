using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<ReviewDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<List<ReviewDto>>.SuccessResponse(data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound(ApiResponse<ReviewDto>.FailResponse("Review not found"));

            return Ok(ApiResponse<ReviewDto>.SuccessResponse(data));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, ApiResponse<ReviewDto>.SuccessResponse(data, "Review created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReviewDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("Review not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Review updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("Review not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Review deleted successfully"));
        }
    }
}
