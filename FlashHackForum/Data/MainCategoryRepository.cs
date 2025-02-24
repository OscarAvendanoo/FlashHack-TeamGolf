using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Data
{
    public class MainCategoryRepository : GenericRepository<MainCategory>, IMainCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public MainCategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<MainCategory> GetByIdIncludeSecondCategory(int id)
        {
            return await _context.MainCategories.Include(m => m.SecondCategories).FirstOrDefaultAsync(m => m.MainCategoryId == id);
        }

        public async Task<MainCategory> GetByNameIncludeSecondCategory(string name)
        {
            return await _context.MainCategories.Include(m => m.SecondCategories).FirstOrDefaultAsync(m => m.Name == name);
        }
    }
}
