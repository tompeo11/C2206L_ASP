using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class Vaccine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        [ValidateNever]
        public VaccineType Type { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public int Price { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
