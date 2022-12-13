using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logiwa.Data.Entities.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateTime = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime CreateTime { get; private set; }
    }
}
