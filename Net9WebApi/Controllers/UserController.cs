using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<List<UserDto>>.SuccessResponse(data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if (data == null)
                return NotFound(ApiResponse<UserDto>.FailResponse("User not found"));

            return Ok(ApiResponse<UserDto>.SuccessResponse(data));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var data = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = data.Id }, ApiResponse<UserDto>.SuccessResponse(data, "User created successfully"));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("User not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "User updated successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
                return NotFound(ApiResponse<bool>.FailResponse("User not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "User deleted successfully"));
        }

        // Bonus: Login handling could be here or separately.
        // For now, standard CRUD.
    }
}
