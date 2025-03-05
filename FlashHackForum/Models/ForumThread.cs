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

        [ForeignKey("SecondCategory")]
        public int SecondCategoryId { get; set; }  // Den här kan vara en foreign key till SecondCategory

        // Navigation property till SecondCategory
        public SecondCategory SecondCategory { get; set; }  // Gör det möjligt att navigera till SecondCategory
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual List<ThreadPost> PostsInThread { get; set; } = new List<ThreadPost>();
        public bool IsAnonymous { get; set; } = false;
    }
}
