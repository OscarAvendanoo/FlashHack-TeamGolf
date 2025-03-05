using FlashHackForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FlashHackForum.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SecondCategory> SecondCategories { get; set; }
        public DbSet<Education> Educations { get; set; } 
        public DbSet<Competens> Competenses { get; set; } 
        public DbSet<ForumThread> ForumThreads { get; set; } 
        public DbSet<ThreadPost> ThreadPosts {  get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompetence> UserCompetences { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User to Account (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Account)
                .WithOne(a => a.User)
                .HasForeignKey<Account>(a => a.UserId)
                .IsRequired();

            // Many-to-Many: Account and ForumThread (Favorites)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Favorites)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "AccountFavorites",
                    j => j
                        .HasOne<ForumThread>()
                        .WithMany()
                        .HasForeignKey("FavoritesForumThreadID")
                        .OnDelete(DeleteBehavior.Cascade), // Deletes Favorite entry when thread is deleted
                    j => j
                        .HasOne<Account>()
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.NoAction) // Prevents cascade conflict
                );

            // One-to-Many: Account to ForumThread (Threads Started)
            modelBuilder.Entity<Account>()
                .HasMany(a => a.ThreadsStarted)
                .WithOne(ft => ft.ThreadCreator)
                .HasForeignKey(ft => ft.CreatorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents deletion of threads when account is deleted

            // One-to-Many: Account to ThreadPosts
            modelBuilder.Entity<Account>()
                .HasMany(a => a.ThreadPosts)
                .WithOne(tp => tp.PostCreator)
                .HasForeignKey(tp => tp.PostCreatorId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes posts when account is deleted

            // One-to-Many: ForumThread to ThreadPosts
            modelBuilder.Entity<ThreadPost>()
                .HasOne(tp => tp.ForumThread)
                .WithMany(ft => ft.PostsInThread)
                .HasForeignKey(tp => tp.ForumThreadId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ Now posts are deleted when thread is deleted

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
       
    }
}
