using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IUserPostReaction
    {
        Task<UserPostReaction?> GetUserPostReaction(int userId, int threadPostId);
        Task AddNewPostReaction(UserPostReaction reaction);
    }
}
