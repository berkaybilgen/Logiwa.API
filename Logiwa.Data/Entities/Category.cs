using Logiwa.Data.Entities.Base;

namespace Logiwa.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int MinimumStockQuantity { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
