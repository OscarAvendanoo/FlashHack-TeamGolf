using FlashHackForum.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByIDIncludeAll(int id);
        Task<Account> GetAccountByUserID(int userId);

        // Hämta Account med relaterad User baserat på userId
        Task<Account> GetAccountWithUserByIdAsync(int userId);
        
        
        Task<IEnumerable<Account>> GetAllFavourites();

        Task<Account> GetAccountByUserIDWithFavorites(int id);
        Task<Account> GetAccountByUserIDIncludeThreadsStarted(int userId);

    }
}
