using Microsoft.EntityFrameworkCore;
using Net9WebApi.Data;
using Net9WebApi.DTOs;
using Net9WebApi.Entities;
using Net9WebApi.Services.Interfaces;

namespace Net9WebApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.Category.Name, p.CreatedAt))
                .ToListAsync();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return null;

            return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.Category.Name, p.CreatedAt);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Need to fetch category name for DTO or return generic
            // For performance, we can load it or just return empty name for now, but better to load.
            var categoryName = await _context.Categories.Where(c => c.Id == dto.CategoryId).Select(c => c.Name).FirstOrDefaultAsync() ?? "Unknown";

            return new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CategoryId, categoryName, product.CreatedAt);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
