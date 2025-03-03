using FlashHackForum.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByIDIncludeAll(int id);

        // Hämta Account med relaterad User baserat på userId
        Task<Account> GetAccountWithUserByIdAsync(int userId);
        
        
    }
}
