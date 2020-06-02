using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core_MVC_Store.Models
{
    public class Products
    {
        public int Id { get; set;  }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool Available { get; set; }
        [MaxLength(300)]
        public string Image { get; set; }
        [MaxLength(50)]
        public string ShadeColor { get; set; }
        
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes ProductTypes { get;  set;  }

        [DisplayName("Special Tag")]
        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        public virtual SpecialTags SpecialTags { get; set; }
    }
}
