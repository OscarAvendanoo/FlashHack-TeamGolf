using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlashHackForum.Models
{
    public class ForumThread
    {
        public int ForumThreadID { get; set; }
        [Required]
        public string Title { get; set; }
        [ForeignKey("ThreadCreator")] // Specifies that CreatorId is the foreign key for the Creator navigation property
        public int CreatorId { get; set; }
        [Required]
        public Account ThreadCreator { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual List<ThreadPost> PostsInThread { get; set; } = new List<ThreadPost>();
}
}
