using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models
{
    public class Brand
    {
        [Key]
        public int intSeqId { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(150)]
        public string varBrandName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? varLogoUrl { get; set; }

        public bool isActive { get; set; } = true;

        public DateTime dtCreatedDate { get; set; }

        public DateTime? dtUpdatedDate { get; set; }
    }
}
