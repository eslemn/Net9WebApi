using Net9WebApi.Entities;

namespace Net9WebApi.Entities
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
