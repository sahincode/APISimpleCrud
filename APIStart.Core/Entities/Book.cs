using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIStart.Core.Entities
{
    public class Book :BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
