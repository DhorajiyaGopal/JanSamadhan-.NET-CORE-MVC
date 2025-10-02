using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JanSamadhan.Models
{
    public class UpdateReplyViewModel
    {
        public int Id { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }

        public IFormFile AttachImage { get; set; }

        public string ExistingAttachImage { get; set; }
    }

}
