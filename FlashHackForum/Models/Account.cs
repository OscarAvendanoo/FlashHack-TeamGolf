using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FlashHackForum.Models
{
    public class Account : User
    {
        public int AccountId { get; set; }
        [Required]
        public bool IsPremium { get; set; }
        public string? Biography { get; set; }
        public string? Signature { get; set; }
        public virtual ICollection<ForumThread> Favorites { get; set; } = new List<ForumThread>();
        public virtual ICollection<ForumThread> ThreadsStarted { get; set; } = new List<ForumThread>();
        public virtual ICollection<ThreadPost> ThreadPosts { get; set; } = new List<ThreadPost>();
        public virtual ICollection<Competens> Competenses { get; set; } = new List<Competens>();
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Employer { get; set; }
        public string? ProfileImage { get; set; }
        public int AccountRating { get; set; } = 0; 

        // properties that declares which information that should be left out or not,
        // will be handled by logics inside razor view.
        public bool ShowAdvertisements { get; set; } = true;
        public bool ShowContact { get; set; } = true;        // sätter allt initialt till true så får användaren ändra detta om den vill
        public bool ShowToCompanies { get; set; } = true;
        public DateTime AccountCreatedAt { get; set; } = DateTime.Now;  
      
        
    }
}
