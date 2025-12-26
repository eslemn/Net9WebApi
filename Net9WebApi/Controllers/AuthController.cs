using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized(ApiResponse<string>.FailResponse("Invalid credentials"));

            return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
        }
    }
}
