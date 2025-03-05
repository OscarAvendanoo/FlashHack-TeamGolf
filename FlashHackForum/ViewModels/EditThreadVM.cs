using FlashHackForum.Models;
using System.Globalization;

namespace FlashHackForum.ViewModels
{
    public class EditThreadVM
    {
        public int ThreadToEditId { get; set; }
        public string Desrciption { get; set; }
        public bool IsAnonymous { get; set; }
        public string FirstPostMessage { get; set; }
    }
}
