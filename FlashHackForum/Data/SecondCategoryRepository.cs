using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class SecondCategoryRepository : GenericRepository<SecondCategory>, ISecondCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public SecondCategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<SecondCategory> GetByCategoryIdIncludeThreads(int id)
        {
            return await _context.SecondCategories.Include(sc => sc.Threads).FirstOrDefaultAsync(sc => sc.SecondCategoryId == id);
        }

        public async Task<SecondCategory> GetByCategoryNameIncludeThreads(string name)
        {
            return await _context.SecondCategories.Include(sc => sc.Threads).FirstOrDefaultAsync(sc => sc.Name == name);
        }

    }
}
