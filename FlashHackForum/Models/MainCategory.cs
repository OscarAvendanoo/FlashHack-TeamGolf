using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.Models
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<SecondCategory> SecondCategories { get; set; } = new List<SecondCategory>();
    }
}
