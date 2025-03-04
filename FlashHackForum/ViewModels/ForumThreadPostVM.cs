using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class ForumThreadPostVM
    {
        public string MainCategoryName { get; set; }
        public List<SubCategoryVM> SecondCategories { get; set; }
    }
}
