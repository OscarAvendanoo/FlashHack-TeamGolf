namespace FlashHackForum.Models
{
    public enum ReactionType
    {
        None,
        Like,
        Dislike
    }
    public class UserPostReaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ThreadPostId { get; set; }
        public ThreadPost ThreadPost { get; set; }
        public ReactionType? ReactionType { get; set; }
    }
}
