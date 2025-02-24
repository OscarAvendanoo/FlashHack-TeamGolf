using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.Models
{
    public class Competens
    {
        public int CompetensId { get; set; }
        public Education? Education { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
