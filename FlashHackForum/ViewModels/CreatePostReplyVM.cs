using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class CreatePostReplyVM
    {
        public ThreadPost PostToReplyTo { get; set; }
        public string PostMessage { get; set; }
        public int ThreadId { get; set; }

        public bool ShowSignature { get; set; }
        public bool Anonymous { get; set; }
    }
}
