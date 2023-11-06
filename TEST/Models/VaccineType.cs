using System.ComponentModel.DataAnnotations;

namespace TEST.Models
{
    public class VaccineType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
