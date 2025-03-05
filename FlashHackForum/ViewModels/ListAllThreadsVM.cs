using FlashHackForum.Models;

namespace FlashHackForum.ViewModels
{
    public class ListAllThreadsVM
    {
        public string ThreadName { get; set; }
        public string ThreadCreator { get; set; }
        public ForumThread LatestThread { get; set; }

        public int ThreadId { get; set; }
        

    }
}
