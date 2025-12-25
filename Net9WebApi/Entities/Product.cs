using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;

namespace Net9WebApi.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation Property
        public Category Category { get; set; } = null!;

        // Navigation Property (Review ilişkisi için)
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
