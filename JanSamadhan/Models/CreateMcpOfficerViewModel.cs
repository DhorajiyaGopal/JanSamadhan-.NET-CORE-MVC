using System.ComponentModel.DataAnnotations;
using System;
namespace JanSamadhan.Models
{
    public class CreateMcpOfficerViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public string Designation { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
