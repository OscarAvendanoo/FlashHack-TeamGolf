using FlashHackForum.Models;
using FLashHackForum.Data;

namespace FlashHackForum.Data
{
    public class EducationRepository : GenericRepository<Education>
    {
        private readonly ApplicationDbContext _context;

        public EducationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }
    }
}
