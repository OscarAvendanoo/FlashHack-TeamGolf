using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.ViewModels
{
    public class RegistrationViewModel
    {
        // User Model Properties
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not Match.")]
        public string ConfirmPassword { get; set; }
        
        // Account Model Properties

        public string Biography {  get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public bool IsPremium { get; set; }

    }
}
