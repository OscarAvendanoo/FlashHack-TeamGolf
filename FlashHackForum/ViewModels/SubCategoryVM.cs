using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class SubCategoryVM
    {
        public string SecondCategoryName { get; set; } 
        public List<ForumThread> ForumThreads { get; set; }
        public int TotalThreads { get; set; }
        public ForumThread? LatestThread { get; set; }
    }
}
