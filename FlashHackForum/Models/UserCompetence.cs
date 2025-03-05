namespace FlashHackForum.Models
{
    public class UserCompetence
    {
        public int AccountId { get; set; }

        public Account Account { get; set; }

        public int CompetensId { get; set; }

        public Competens Competens { get; set; }

        public int Grade { get; set; }

        public Education? Education { get; set; }
    }
}
