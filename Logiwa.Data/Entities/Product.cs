using System.ComponentModel.DataAnnotations.Schema;
using Logiwa.Data.Entities.Base;

namespace Logiwa.Data.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }
    }
}
