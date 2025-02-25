using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class CreateSubCategoryViewModel
    {
        public int MainCategoryId { get; set; }
        public List<MainCategory> MainCategories { get; set; }
    }
}
