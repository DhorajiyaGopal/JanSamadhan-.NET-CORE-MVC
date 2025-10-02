using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class CreateUserViewModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required, StringLength(50, MinimumLength = 6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(200)]
        public string Address { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
