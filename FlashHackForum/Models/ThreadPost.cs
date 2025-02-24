using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashHackForum.Models
{
    public class ThreadPost
    {
        public int ThreadPostId { get; set; }
        [Required]
        [ForeignKey("PostCreator")] 
        public int PostCreatorId { get; set; }
        public Account PostCreator { get; set; }
        [Required]
        public string PostMessage { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
    }
}
