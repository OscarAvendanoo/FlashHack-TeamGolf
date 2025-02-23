using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FlashHackForum.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        [Required]
        public string Name { get; set; }
        // ej säker på om jag förstår nedanstående, kolla med gänget :)
        public string Profile { get; set; }
        public string? Logo { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Competens> CompetensesToLookFor { get; set; } = new List<Competens>();
    }
}
