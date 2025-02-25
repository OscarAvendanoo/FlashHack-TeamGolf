using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByIDIncludeAll(int id);

    }
}
