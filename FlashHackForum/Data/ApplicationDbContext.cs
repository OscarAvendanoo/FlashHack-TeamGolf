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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Account and ForumThread for "Favorites"
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Favorites)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "AccountFavorites",
                    j => j
                        .HasOne<ForumThread>()
                        .WithMany()
                        .HasForeignKey("FavoritesForumThreadID")
                        .OnDelete(DeleteBehavior.Cascade), // Cascade delete when ForumThread is deleted
                    j => j
                        .HasOne<Account>()
                        .WithMany()
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.NoAction) // Prevent multiple cascade paths
                );

            // Configure the relationship between Account and ForumThread for "ThreadsStarted"
            modelBuilder.Entity<Account>()
                .HasMany(a => a.ThreadsStarted)
                .WithOne(ft => ft.ThreadCreator)
                .HasForeignKey(ft => ft.CreatorId);

            // Configure the relationship between Account and ThreadPost
            modelBuilder.Entity<Account>()
                .HasMany(a => a.ThreadPosts)
                .WithOne(tp => tp.PostCreator)
                .HasForeignKey(tp => tp.PostCreatorId);

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
       
    }
}
