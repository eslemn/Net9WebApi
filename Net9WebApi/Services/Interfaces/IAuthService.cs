using Net9WebApi.DTOs;

namespace Net9WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto dto);
    }
}
