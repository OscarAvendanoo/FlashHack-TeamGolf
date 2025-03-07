using FlashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using System.Drawing.Printing;
using X.PagedList;
using X.PagedList.Extensions;

namespace FlashHackForum.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IForumThreadRepository forumThreadRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IUserRepository userRepository;
        private readonly IThreadPostRepository threadPostRepository;
        private readonly ISecondCategoryRepository secondCategoryRepository;

        public FavouriteController(IForumThreadRepository forumThreadRepository, IAccountRepository accountRepository, IUserRepository userRepository, IThreadPostRepository threadPostRepository, ISecondCategoryRepository secondCategoryRepository)
        {
            this.forumThreadRepository = forumThreadRepository;
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
            this.threadPostRepository = threadPostRepository;
            this.secondCategoryRepository = secondCategoryRepository;

        }

        /*
         Här jag har gjort en pageination och begränsat det till bara 4 stycken forumtrådar per sida för att kunna kontrollera att pagineringen fungerade.
        Det är lätt att ändra "pageSize" till så många forumtrådar man vill visa per sida.
         */


        public async Task<ActionResult> ListAllFavourites(int? page)
        {

            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var forumThreads = new List<ForumThread>();
            var accountWithFavourites = await accountRepository.GetAllFavourites();

            foreach (var account in accountWithFavourites)
            {
                forumThreads.AddRange(account.Favorites);
            }

            return View(forumThreads.ToPagedList(pageNumber, pageSize));
        }


        public async Task<ActionResult> ListMyFavourites(int? page, int userId)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var userName = HttpContext.Session.GetString("UserName");
            var user = await userRepository.GetUserByUsername(userName);
            var account = await accountRepository.GetAccountByUserIDWithFavorites(user.UserId);


            if (account == null)
            {
                return NotFound();
            }

            var favourites = account.Favorites;

            return View(favourites.ToPagedList(pageNumber, pageSize));

        }

        /*En lista av alla threads som användaren har skapat själv*/

        public async Task<ActionResult> ListMyThreads(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var userName = HttpContext.Session.GetString("UserName");
            var user = await userRepository.GetUserByUsername(userName);

            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            var userID = HttpContext.Session.GetInt32("UserId");
            var account = await accountRepository.GetAccountByUserIDIncludeThreadsStarted(userID.Value);

            var myThreads = account.ThreadsStarted;

            return View(myThreads.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> ListAllThreads(int? page, int? id, string? secondCategoryName)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var forumThreads = new List<ForumThread>();

            if (id != null)
            {
                var secondCategory = await secondCategoryRepository.GetByCategoryIdIncludeThreads((int)id);

                foreach (var item in secondCategory.Threads)
                {
                    forumThreads.Add(item);
                }
                ViewBag.SecondCategory = secondCategory.Name;
            }
            else
            {
                var secondCategory = await secondCategoryRepository.GetByCategoryNameIncludeThreads(secondCategoryName);

                foreach (var item in secondCategory.Threads)
                {
                    forumThreads.Add(item);
                }
                ViewBag.SecondCategory = secondCategory.Name;
            }


            return View(forumThreads.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> ListAllThreadsByCategory(int? page, int? id, string? secondCategoryName)
        {

            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            // Hämta kategorin baserat på id eller namn
            var secondCategory = id.HasValue
                ? await secondCategoryRepository.GetByCategoryIdIncludeThreads(id.Value)
                : await secondCategoryRepository.GetByCategoryNameIncludeThreads(secondCategoryName);
            var allThreads = await forumThreadRepository.GetAllIncludePostsAndCreators();

            if (secondCategory == null || secondCategory.Threads == null)
            {
                return NotFound(); 
            }

            
            ViewBag.SecondCategory = secondCategory.Name;

           
            var vmList = secondCategory.Threads
                .OrderByDescending(t => t.CreatedAt)
                .Select(forumThread => new ListAllThreadsVM
                {
                    LatestThread = secondCategory.Threads.FirstOrDefault(),
                    ThreadCreator = forumThread.ThreadCreator.DisplayName,
                    ThreadName = forumThread.Title,

                    ThreadId = forumThread.ForumThreadID
                })
                .ToList();


            return View(vmList.ToPagedList(pageNumber, pageSize));


            //    int pageSize = 10;
            //    int pageNumber = page ?? 1;
            //    ViewBag.CurrentPage = pageNumber;


            //    var secondCategory = id.HasValue
            //? await secondCategoryRepository.GetByCategoryIdIncludeThreads(id.Value)
            //: await secondCategoryRepository.GetByCategoryNameIncludeThreads(secondCategoryName);


            //    var allThreads = await forumThreadRepository.GetAllIncludePostsAndCreators();
            //    List<ListAllThreadsVM> vmList = new List<ListAllThreadsVM>();

            //    foreach (var forumThread in secondCategory.Threads)
            //    {
            //        var listAllThreadsVM = new ListAllThreadsVM
            //        {

            //            LatestThread = secondCategory.Threads.Take(1).OrderByDescending(s => s.CreatedAt).FirstOrDefault(),
            //            ThreadCreator = forumThread.ThreadCreator.DisplayName,
            //            ThreadName = forumThread.Title,
            //            ThreadId = forumThread.ForumThreadID,

            //        };

            //        ViewBag.SecondCategory = secondCategory.Name;
            //        vmList.Add(listAllThreadsVM);
            //    }
            //    return View(vmList.ToPagedList(pageNumber, pageSize));
        }
        
        [HttpGet("thread/{threadId}/is-favorite")]
        public async Task<IActionResult> IsFavorite(int threadId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized();
            }

            var account = await accountRepository.GetAccountByUserIDWithFavorites((int)userId);
            var thread = await forumThreadRepository.GetByIDAsync(threadId);

            if (account == null || thread == null)
            {
                return NotFound();
            }

            bool isFavorite = account.Favorites.Contains(thread);
            return Ok(new { isFavorite });
        }

        [HttpPost("thread/{threadId}/favorite")]
        public async Task<IActionResult> ChangeFavStatus(int threadId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var account = await accountRepository.GetAccountByUserIDWithFavorites((int)userId);
            var thread = await forumThreadRepository.GetByIDAsync(threadId);
            if (account.Favorites.Contains(thread))
            {
                account.Favorites.Remove(thread);
            }
            
            else
            {
                account.Favorites.Add(thread);

            }
            await accountRepository.SaveChanges();
            return Ok();

        }
        
    }
}
