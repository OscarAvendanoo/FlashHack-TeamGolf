using Microsoft.EntityFrameworkCore;
using FlashHackForum.Data;
using FlashHackForum.Models;
using FlashHackForum.Data.Interfaces;

namespace FlashHackForum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Sesson state setup
            builder.Services.AddMemoryCache();
            builder.Services.AddSession(options =>
            {
                // Auto-end session after 10min of user idling
                options.IdleTimeout = TimeSpan.FromSeconds(60 * 10);
                options.Cookie.IsEssential = true;
            });

            // Add services to the container.

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Oscar")));

            // Services här under ärver det generiska interfacet, så dessa kan man säga är två kombinerade interface, ett generiskt och ett med "include" metoder i ett och samma interface
            // Tänk på att lägga till dem på samma sätt i i era controllers :)
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
            builder.Services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();
            builder.Services.AddScoped<ISecondCategoryRepository, SecondCategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IThreadPostRepository, ThreadPostRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWorkRepository>();


            // Services här under har endast det generiska repositoryt "IRepository", alltså inga "Include" metoder.
            // Om ni vill lägga till include metoder till dessa så behöver dem implementeras som Services här ovan, kika i interfacemappen samt repo-filer om ni undrar hur det fungerar.
            // Annars bara att säga till så visar jag :)
            builder.Services.AddScoped<IRepository<Education>, EducationRepository>();
            builder.Services.AddScoped<IRepository<Competens>, CompetensRepository>();

            //Regitrering av session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            // enable session state
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{pageNumber?}");
            });

            app.Run();

            //var user = new User
            //{
            //    FirstName = "Kevin",
            //    LastName = "Hedendahl",
            //    UserName = "KekeKetamin",
            //    IsAdmin = false,
            //    Email = "kevin@kevin.com",
            //    Password = "Kevin"

            //};

            //var account = new Account
            //{
            //    IsPremium = false,
            //    DisplayName = "Kevin",
            //    User = user,
            //};
            //var threadPost = new ThreadPost
            //{
            //    PostCreator = account,
            //    PostMessage = "Hejsan"
            //};

            //var thread = new ForumThread
            //{
            //    Title = "Här är en titel",
            //    ThreadCreator = account,

            //};

            //thread.PostsInThread.Add(threadPost);
            //account.Favorites.Add(thread);

            

            ////await _accountRepository.AddAsync(account);

            //var mainCategory = new MainCategory
            //{
            //    Name = "Spel"
            //};

            //var secondCategory = new SecondCategory
            //{
            //    Name = "World of Warcraft"
            //};

            //mainCategory.SecondCategories.Add(secondCategory);
            ////await _mainCategoryRepository.AddAsync(mainCategory);


        }
    }
}
