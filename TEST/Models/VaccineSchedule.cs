using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class VaccineSchedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string VaccinationDates { get; set; }

        public int VaccineId { get; set; }
        [ForeignKey("VaccineId")]
        [ValidateNever]
        public Vaccine Vaccine { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
