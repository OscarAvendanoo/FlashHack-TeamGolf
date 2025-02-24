using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FlashHackForum.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string ProgramName { get; set; }
        [Required]
        public string LengthOfEducation { get; set; }
        public string? YearStarted  { get; set; } // visste ej om jag skulle sätta dessa som int eller string, beror lite på hur dem matas in från vyn sen, får casta annars :)
        public string? YearEnded { get; set; }
        public string? Description { get; set; }
    }
}
