using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class UpdateIssueViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        public IFormFile RelatedImage { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}