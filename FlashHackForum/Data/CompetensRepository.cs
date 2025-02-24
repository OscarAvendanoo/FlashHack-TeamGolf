using FlashHackForum.Models;
using FLashHackForum.Data;

namespace FlashHackForum.Data
{
    public class CompetensRepository : GenericRepository<Competens>
    {
        private readonly ApplicationDbContext _context;

        public CompetensRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }
    }
}
