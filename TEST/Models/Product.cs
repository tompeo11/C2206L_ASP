﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEST.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        [Required]
        public string ISBN { get; set; } = "";

        [Required]
        public string Author { get; set; } = "";

        [Required]
        public double Price { get; set; }

        public double? Price50 { get; set; }

        public double? Price100 { get; set; }

        public string ImageUrl { get; set; } = "default.jpg";

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }
    }
}
