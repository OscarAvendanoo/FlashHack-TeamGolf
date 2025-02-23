using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
