using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class CreateReplyViewModel
    {
        [Required, StringLength(500)]
        public string Description { get; set; }

        public IFormFile AttachImage { get; set; }

        [Required]
        public int McpOfficerId { get; set; }

        [Required]
        public int IssueId { get; set; }
    }
}
