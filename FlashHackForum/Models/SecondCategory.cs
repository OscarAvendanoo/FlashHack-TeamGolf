using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FlashHackForum.Models
{
    public class SecondCategory
    {
        public int SecondCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public virtual List<ForumThread> Threads { get; set; } = new List<ForumThread>();
    }
}
