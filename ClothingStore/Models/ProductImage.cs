using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    [Table("tblProductImages")]
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        [StringLength(500)]
        public string? ImagePath { get; set; }

        public bool IsMain { get; set; }

        public int DisplayOrder { get; set; }
        // Navigation Property
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
