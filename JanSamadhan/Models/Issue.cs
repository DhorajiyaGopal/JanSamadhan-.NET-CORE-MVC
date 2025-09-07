using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JanSamadhan.Models
{
    public class Issue
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot be longer than 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public IList<string> RelatedImageUrl { get; set; } = new List<string>();
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
