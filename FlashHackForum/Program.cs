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

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Jonathan")));

            // Services här under ärver det generiska interfacet, så dessa kan man säga är två kombinerade interface, ett generiskt och ett med "include" metoder i ett och samma interface
            // Tänk på att lägga till dem på samma sätt i i era controllers :)
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IForumThreadRepository, ForumThreadRepository>();
            builder.Services.AddScoped<IMainCategoryRepository,MainCategoryRepository>();
            builder.Services.AddScoped<ISecondCategoryRepository, SecondCategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IThreadPostRepository, ThreadPostRepository>();

            // Services här under har endast det generiska repositoryt "IRepository", alltså inga "Include" metoder.
            // Om ni vill lägga till include metoder till dessa så behöver dem implementeras som Services här ovan, kika i interfacemappen samt repo-filer om ni undrar hur det fungerar.
            // Annars bara att säga till så visar jag :)
            builder.Services.AddScoped<IRepository<Education>,EducationRepository>();
            builder.Services.AddScoped<IRepository<Competens>, CompetensRepository>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();


        }
    }
}
