using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;

namespace Net9WebApi.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        // Navigation Property
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
