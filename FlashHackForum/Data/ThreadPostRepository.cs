using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class ThreadPostRepository :GenericRepository<ThreadPost>, IThreadPostRepository
    {
        private readonly ApplicationDbContext _context;

        public ThreadPostRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

  
        public async Task<ICollection<ThreadPost>> GetAllPostsByAccountID(int id)
        {
            return await _context.ThreadPosts.Where(tp => tp.PostCreatorId == id).ToListAsync();
        }

     
    }
}
