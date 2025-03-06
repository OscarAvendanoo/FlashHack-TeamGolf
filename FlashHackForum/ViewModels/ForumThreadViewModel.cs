using FlashHackForum.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlashHackForum.ViewModels
{
    public class ForumThreadViewModel
    {
        [Required(ErrorMessage = "Titel är obligatorisk")]
        [Display(Name = "Titel")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori måste väljas")]
        [Display(Name = "Kategori")]
        // public int SelectedCategoryId { get; set; }

        public List<SecondCategory>? SecondCategories { get; set; } = new List<SecondCategory>();

        [HiddenInput(DisplayValue = false)]
        public int? CreatorId { get; set; } // Användarens ID som skickas in från inloggad användare

        // Lägg till SecondCategoryId här
        [Required(ErrorMessage = "En andra kategori måste väljas")]
        [Display(Name = "Second Category")]
        public int SecondCategoryId { get; set; }

        public bool ShowSignature { get; set; }
        public bool Anonymous { get; set; }

        // Lägg till CreatedAt här, om du vill ha den som ett dold fält (den kan sättas i controller)
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Sätt ett standardvärde här om du vill


        public ThreadPostViewModel ThreadPost { get; set; } // Använd en enkel ViewModel för ThreadPost

        public bool Anonymous { get; set; }
        //public ThreadPost? ThreadPost { get; set; } // Hela objektet!
    }
    public class ThreadPostViewModel
    {
        [Required(ErrorMessage = "Inläggsmeddelande är obligatoriskt")]
        [Display(Name = "Meddelande")]
        public string PostMessage { get; set; } = string.Empty;
    }
}
