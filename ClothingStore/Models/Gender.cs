using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    [Table("tblGenders")]
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }

        [StringLength(100)]
        public string? GenderName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
