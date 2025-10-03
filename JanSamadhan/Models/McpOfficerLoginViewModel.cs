using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class McpOfficerLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
