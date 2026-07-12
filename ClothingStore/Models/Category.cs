using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    [Table("tblCategories")]
    public class Category
    {
        [Key]
        [Required]

        public int intSeqId { get; set; }

        [MaxLength(150)]
        public string? varName { get; set; }

        [MaxLength(500)]
        public string? varDescription { get; set; }

          public bool? IsActive { get; set; }

        public DateTime? dtCreatedDate { get; set; }

        public DateTime? dtUpdatedDate { get; set; }
    }
}
//-intSeqId INT PRIMARY KEY,
//--    varName NVARCHAR(200) NULL,
//--    varDescription NVARCHAR(MAX) NULL,
//--    intIsActive INT DEFAULT 1,
//--    dtCreatedDate DATETIME DEFAULT GETDATE(),
//--    dtUpdatedDate DATETIME NULL