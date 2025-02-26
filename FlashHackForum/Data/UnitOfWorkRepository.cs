using FlashHackForum.Data.Interfaces;

namespace FlashHackForum.Data
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);
            AccountRepository = new AccountRepository(_context);
        }

        public IUserRepository UserRepository { get; }

        public IAccountRepository AccountRepository {  get; }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
