using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TEST.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("CoverType Name")]
        [MaxLength(50)]
        [Required]
        public string? Name { get; set; }
    }
}
