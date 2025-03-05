using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IForumThreadRepository : IRepository<ForumThread>
    {
        Task<ForumThread> GetByIdIncludePostsAndCreators(int id);
        Task<IEnumerable<ForumThread>> GetAllIncludePostsAndCreators();
    }
}
