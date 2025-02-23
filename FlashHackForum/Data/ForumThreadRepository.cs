using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class ForumThreadRepository : GenericRepository<ForumThread>, IForumThreadRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumThreadRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        // Hämtar allt från tråden via dess ID, inkluderar alla posts, vem som skapat dessa samt vem som skapat tråden. 
        public async Task<ForumThread> GetByIdIncludePostsAndCreators(int id)
        {
            return await _context.ForumThreads.Include(ft => ft.ThreadCreator).Include(ft => ft.PostsInThread).ThenInclude(p => p.PostCreator).FirstOrDefaultAsync(ft => ft.ForumThreadID == id);
        }
    }
}
