using Microsoft.EntityFrameworkCore;
using Net9WebApi.Data;
using Net9WebApi.DTOs;
using Net9WebApi.Entities;
using Net9WebApi.Services.Interfaces;

namespace Net9WebApi.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReviewDto>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Select(r => new ReviewDto(r.Id, r.Rating, r.Comment, r.UserId, r.User.Username, r.ProductId, r.Product.Name, r.CreatedAt))
                .ToListAsync();
        }

        public async Task<ReviewDto?> GetByIdAsync(int id)
        {
            var r = await _context.Reviews
                .Include(x => x.User)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (r == null) return null;

            return new ReviewDto(r.Id, r.Rating, r.Comment, r.UserId, r.User.Username, r.ProductId, r.Product.Name, r.CreatedAt);
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto dto)
        {
            var review = new Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Fetch names for DTO
            var username = await _context.Users.Where(u => u.Id == dto.UserId).Select(u => u.Username).FirstOrDefaultAsync() ?? "Unknown";
            var productName = await _context.Products.Where(p => p.Id == dto.ProductId).Select(p => p.Name).FirstOrDefaultAsync() ?? "Unknown";

            return new ReviewDto(review.Id, review.Rating, review.Comment, review.UserId, username, review.ProductId, productName, review.CreatedAt);
        }

        public async Task<bool> UpdateAsync(int id, UpdateReviewDto dto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            review.IsDeleted = true;
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReviewDto>> GetByProductIdAsync(int productId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .Select(r => new ReviewDto(r.Id, r.Rating, r.Comment, r.UserId, r.User.Username, r.ProductId, r.Product.Name, r.CreatedAt))
                .ToListAsync();
        }

        public async Task<List<ReviewDto>> GetByUserIdAsync(int userId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .Select(r => new ReviewDto(r.Id, r.Rating, r.Comment, r.UserId, r.User.Username, r.ProductId, r.Product.Name, r.CreatedAt))
                .ToListAsync();
        }
    }
}
