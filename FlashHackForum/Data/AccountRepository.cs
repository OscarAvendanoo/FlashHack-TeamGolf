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

        public Task<Account> GetAccountByUserEmailAsync(string email)
        {
            throw new NotImplementedException();
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
    }

}