using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public string ProfilePictureUrl { get; set; }

        // optional: password update can be handled separately
    }
}
