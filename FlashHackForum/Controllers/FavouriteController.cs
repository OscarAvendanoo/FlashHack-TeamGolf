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

        public FavouriteController(IForumThreadRepository forumThreadRepository)
        {
            this.forumThreadRepository = forumThreadRepository;
        }
        public async Task<ActionResult> ListAllFavourites(int? page)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;

            var items = await forumThreadRepository.GetAllAsync();


            ViewBag.CurrentPage = pageNumber;

            return View(items.ToPagedList(pageNumber, pageSize));
        }
    }
}
