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

        public async Task<Account> GetAccountByUserEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.Email == email);
        }

        // inkluderar alla virituella listor samt education i competenses, vill man ha mer data än så får man göra ett till anrop till databasen med t.ex trådens ID
        public async Task<Account> GetAccountByUserEmailIncludeAllAsync(string email)
        {
            return await _context.Accounts.Include(u => u.Competenses).ThenInclude(a => a.Education).Include(a => a.Favorites).
                Include(a => a.ThreadsStarted).Include(a=> a.ThreadPosts).FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Account> GetAccountByUserNameAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.UserName == username);
        }
        // inkluderar alla virituella listor samt education i competenses, vill man ha mer data än så får man göra ett till anrop till databasen med t.ex trådens ID
        public async Task<Account> GetAccountByUserNameIncludeAllAsync(string username)
        {
            return await _context.Accounts.Include(u => u.Competenses).ThenInclude(a => a.Education).Include(a => a.Favorites).
                Include(a => a.ThreadsStarted).Include(a => a.ThreadPosts).FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
