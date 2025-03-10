using FlashHackForum.Models;
using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.ViewModels
{
    public class ChangePasswordViewModel
    {
        public User User { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
