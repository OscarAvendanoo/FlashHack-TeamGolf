using Microsoft.Extensions.Hosting;
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

        [ForeignKey("ForumThread")]
        public int ForumThreadId { get; set; }
        public ForumThread ForumThread { get; set; } // Navigationsproperty
                                                     
        public int? ReplyToPostId { get; set; }  // Foreign key (nullable)
        public ThreadPost ReplyToPost { get; set; }    // Navigation property to the post being replied to
        public bool ShowSignature { get; set; } = false;

        public bool Anonymous { get; set; } = false;
    }
}
