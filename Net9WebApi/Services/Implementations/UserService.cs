using Microsoft.EntityFrameworkCore;
using Net9WebApi.Data;
using Net9WebApi.DTOs;
using Net9WebApi.Entities;
using Net9WebApi.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Net9WebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Username, u.Email, u.CreatedAt))
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            return new UserDto(user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.CreatedAt);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto(user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.CreatedAt);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> VerifyUserAsync(LoginDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username && u.PasswordHash == hashedPassword);

            if (user == null) return null;

            return new UserDto(user.Id, user.FirstName, user.LastName, user.Username, user.Email, user.CreatedAt);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
