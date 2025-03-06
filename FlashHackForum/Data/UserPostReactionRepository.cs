using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class UserPostReactionRepository : IUserPostReaction
    {
        private readonly ApplicationDbContext _context;

        public UserPostReactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserPostReaction?> GetUserPostReaction(int userId, int threadPostId)
        {
            return await _context.UserPostReactions
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ThreadPostId == threadPostId);
        }
        public async Task AddNewPostReaction(UserPostReaction reaction) 
        {
            await _context.UserPostReactions.AddAsync(reaction);
            await _context.SaveChangesAsync();
        }
    }
}
