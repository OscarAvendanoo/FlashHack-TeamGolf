using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IThreadPostRepository : IRepository<ThreadPost>
    {
        Task<ICollection<ThreadPost>> GetAllPostsByAccountID(int id);
    }
}
