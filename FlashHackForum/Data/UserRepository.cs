using FlashHackForum.Models;
using FLashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
