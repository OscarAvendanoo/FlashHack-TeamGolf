using FlashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using Microsoft.AspNetCore.Mvc;
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

            int pageSize = 4;
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
            int pageSize = 4;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var userName = HttpContext.Session.GetString("UserName");
            var user = await userRepository.GetUserByUsername(userName);
            var account = await accountRepository.GetAccountByIDWithFavorites((int)user.AccountId);

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
            int pageSize = 4;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var userName = HttpContext.Session.GetString("UserName");
            var user = await userRepository.GetUserByUsername(userName);

            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var account = await accountRepository.GetAccountByIDIncludeAll((int)user.AccountId);
            var myThreads = account.ThreadsStarted;

            return View(myThreads.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> ListAllThreads(int? page, int id)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            ViewBag.CurrentPage = pageNumber;

            var forumThreads = new List<ForumThread>();
            var secondCategory = await secondCategoryRepository.GetByCategoryIdIncludeThreads(id);

            foreach(var item in secondCategory.Threads)
            {
                forumThreads.Add(item);
            }

            ViewBag.SecondCategory = secondCategory.Name;


            return View(forumThreads.ToPagedList(pageNumber, pageSize));
        }
    }
}
