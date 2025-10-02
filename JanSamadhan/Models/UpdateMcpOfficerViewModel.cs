using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class UpdateMcpOfficerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public string Designation { get; set; }

        // optional: password update can be separate
    }
}
