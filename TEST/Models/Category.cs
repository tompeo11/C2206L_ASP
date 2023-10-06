﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TEST.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [DisplayName("My display order")]
        public int? DisplayOrder { get; set; }


        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
