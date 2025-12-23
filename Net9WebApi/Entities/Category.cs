using System.Collections.Generic;

namespace Net9WebApi.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        // Navigation Property
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
