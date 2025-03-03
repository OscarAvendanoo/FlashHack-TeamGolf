using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }


        public async Task<Account> GetAccountByIDIncludeAll(int id)
        {
            return await _context.Accounts.Include(u => u.Competenses).ThenInclude(a => a.Education).Include(a => a.Favorites).
                Include(a => a.ThreadsStarted).Include(a => a.ThreadPosts).FirstOrDefaultAsync(u => u.AccountId == id);
        }

        // Hämta Account med relaterad User baserat på userId
        public async Task<Account> GetAccountWithUserByIdAsync(int userId)
        {
            // Hämtar Account och inkluderar den relaterade User (via navigation properties)
            var account = await _context.Accounts
                .Include(a => a.User) // Om du har en navigation property till User
                .FirstOrDefaultAsync(a => a.UserId == userId); // Eller något annat relevant fält om det inte är UserId

            if (account == null)
            {
                throw new InvalidOperationException($"Kunde inte hitta konto för användare med ID {userId}");
            }

            return account;
        }

        public Task<Account> GetAccountByUserEmailIncludeAllAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetAccountByUserNameIncludeAllAsync(string username)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Account>> GetAllFavourites()
        {
            return await _context.Accounts.Include(f => f.Favorites).ToListAsync();
        }

        public async Task<Account> GetAccountByIDWithFavorites(int id)
        {
            return await _context.Accounts
                .Include(a => a.Favorites)
                .FirstOrDefaultAsync(a => a.AccountId == id);
        }








    }

}