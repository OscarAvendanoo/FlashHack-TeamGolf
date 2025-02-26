using FlashHackForum.Models;
using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.ViewModels
{
    public class CreateEditSubCategoryViewModel
    {
        [Required(ErrorMessage ="You have to pick a main category")]
        public int? MainCategoryId { get; set; }

        [Required(ErrorMessage = "You have to enter a name for sub category")]
        [StringLength(40, MinimumLength =2)]
        public string? Name { get; set; }

        public IEnumerable<MainCategory>? MainCategories { get; set; }

    }

}
