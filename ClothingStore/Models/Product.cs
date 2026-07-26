using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models
{
    [Table("tblProducts")]
    public class Product
    {
        
            [Key]
            public int intSeqId { get; set; }

            public int? intCategoryId { get; set; }

            [StringLength(250)]
            public string? varName { get; set; }

            [StringLength(50)]
            public string? varProductCode { get; set; }

            public string? varDescription { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [Column(TypeName = "decimal(18,2)")]
            public decimal? flPrice { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal? flDiscountPrice { get; set; }

            public int? intQuantity { get; set; }

            [StringLength(100)]
            public string? varBrand { get; set; }

            [StringLength(50)]
            public string? varSize { get; set; }

            [StringLength(50)]
            public string? varColor { get; set; }

            [StringLength(500)]
            public string? varImageUrl { get; set; }

            public bool? isFeatured { get; set; }

            public bool? isActive { get; set; }

            public DateTime? dtCreatedDate { get; set; }

            public DateTime? dtUpdatedDate { get; set; }

            // Navigation Property
            [ForeignKey("intCategoryId")]
            public virtual Category? Category { get; set; }
     

}
}
