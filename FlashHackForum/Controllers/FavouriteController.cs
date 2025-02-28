using FlashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace FlashHackForum.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IForumThreadRepository forumThreadRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IUserRepository userRepository;

        public FavouriteController(IForumThreadRepository forumThreadRepository, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            this.forumThreadRepository = forumThreadRepository;
            this.accountRepository = accountRepository;
            this.userRepository = userRepository;
        }

        /*
         Här jag har gjort en pageination och begränsat det till bara 4 stycken forumtrådar per sida för att kunna kontrollera att pagineringen fungerade.
        Det är lätt att ändra "pageSize" till så många forumtrådar man vill visa per sida.
         */

        public async Task<ActionResult> ListAllFavourites(int? page)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;


            var userName = HttpContext.Session.GetString("UserName");
            var user = await userRepository.GetUserByUsername(userName);
            var account = accountRepository.GetAccountByIDIncludeAll((int)user.AccountId).Result;

            var favourites = account.Favorites;

            // Har man inga favoriter lagrat än så hämtar man de som har lagrats i sitt konto efter man har lagrat en favorit.

            if (favourites == null)
            {
                var accountId = account.AccountId;
                var forumthread = forumThreadRepository.GetAllAsync().Result;
                foreach (var item in forumthread)
                {
                    item.ThreadCreator.AccountId = accountId;
                    account.Favorites.Add(item);

                }
                accountRepository.SaveChanges();
            }

            ViewBag.CurrentPage = pageNumber;

            return View(favourites.ToPagedList(pageNumber, pageSize));

        }
    }
}
