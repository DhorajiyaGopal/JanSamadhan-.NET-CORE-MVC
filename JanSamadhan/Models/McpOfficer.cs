using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class McpOfficer
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Phone Number is required")]
        [MaxLength(10, ErrorMessage ="Phone Number cannot be more than 10 digits"),
            MinLength(10,ErrorMessage = "Phone Number cannot be less than 10 digits ")]
        public string PhoneNumber { get; set; }

        public string? Designation { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [MinLength(6,ErrorMessage ="Password must be at least 6 characters long")]
        public string Password { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
