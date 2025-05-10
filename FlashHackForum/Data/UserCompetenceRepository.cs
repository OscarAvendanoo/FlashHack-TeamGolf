using FlashHackForum.Models;
using FLashHackForum.Data;

namespace FlashHackForum.Data
{
    public class UserCompetenceRepository : GenericRepository<UserCompetence>
    {
        private readonly ApplicationDbContext _context;

        public UserCompetenceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }
    }
}
