using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class AddCompetenceViewModel
    {
        public int AccountId { get; set; }

        public Account Account { get; set; }

        public IEnumerable<Competens> Competenses { get; set; } // List of available competences

        public List<UserCompetence> UserCompetences { get; set; } // User selected competences and their rankings
    }
}
