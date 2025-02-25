using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.ViewModels
{
    public class RegistrationViewModel
    {
        // User Model Properties
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not Match.")]
        public string ConfirmPassword { get; set; }
        
        // Account Model Properties

        public string Biography {  get; set; }
        public string PhoneNumber { get; set; }
        public string DisplayName { get; set; }
        public bool IsPremium { get; set; }

    }
}
