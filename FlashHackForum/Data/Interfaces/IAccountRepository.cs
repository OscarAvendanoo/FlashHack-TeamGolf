using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByUserNameAsync(string username);
        Task<Account> GetAccountByUserEmailAsync(string email);

        Task<Account> GetAccountByUserNameIncludeAllAsync(string username);

        Task<Account> GetAccountByUserEmailIncludeAllAsync(string email);

    }
}
