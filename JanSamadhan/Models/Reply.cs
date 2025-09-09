using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class Reply
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Reply description is required")]
        [StringLength(500, ErrorMessage = "Reply cannot be longer than 500 characters")]
        public string Description { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated At")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;


        public string AttachImage { get; set; }

        [Required(ErrorMessage = "Officer Id is required")]
        public int McpOfficerId { get; set; }
        public McpOfficer McpOfficer { get; set; }
        [Required(ErrorMessage = "Issue Id is required")]
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}
