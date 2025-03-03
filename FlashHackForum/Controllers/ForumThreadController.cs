using FlashHackForum.Models;
using FlashHackForum.Data.Interfaces;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlashHackForum.Controllers
{
    public class ForumThreadController : Controller
    {
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly ISecondCategoryRepository _secondCategoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IThreadPostRepository _threadPostRepository;

        public ForumThreadController(IForumThreadRepository forumThreadRepository, ISecondCategoryRepository secondCategoryRepository, IAccountRepository accountRepository, IUserRepository userRepository, IThreadPostRepository threadPostRepository)
        {
            _forumThreadRepository = forumThreadRepository;
            _secondCategoryRepository = secondCategoryRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _threadPostRepository = threadPostRepository;
        }

        // Visa alla trådar
        public async Task<IActionResult> Index()
        {
            var threads = await _forumThreadRepository.GetAllAsync();
            return View(threads);
        }

        // GET: ForumThread/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Hämta Account via UserId (via sessionen)
            var account = _accountRepository.GetAccountWithUserByIdAsync(userId.Value).Result;

            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var secondCategories = _secondCategoryRepository.GetAllAsync().Result;

            ViewBag.SecondCategoryId = new SelectList(secondCategories, "Id", "Name");

            // Skapa viewModel och sätt CreatorId till AccountId
            var viewModel = new ForumThreadViewModel
            {
                SecondCategories = _secondCategoryRepository.GetAllAsync().Result.ToList(),
            };

            return View(viewModel);
        }

        // POST: ForumThread/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ForumThreadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.SecondCategories = _secondCategoryRepository.GetAllAsync().Result.ToList();
                return View(viewModel);
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //var user = await _userRepository.GetUserByUsername(HttpContext.Session.GetString("UserName"));
            var account = await _accountRepository.GetAccountWithUserByIdAsync(userId.Value);

            if (account == null)
            {
                return RedirectToAction("Login", "Account");

            }

            var SecondCategories = await _secondCategoryRepository.GetAllAsync();
            viewModel.SecondCategories = SecondCategories.ToList();


            // Kontrollera om vald kategori är giltig
            if (viewModel.SecondCategoryId == null || !viewModel.SecondCategories.Any(c => c.SecondCategoryId == viewModel.SecondCategoryId))
            {
                ModelState.AddModelError("SelectedCategoryId", "Vänligen välj en giltig kategori.");
                viewModel.SecondCategories = _secondCategoryRepository.GetAllAsync().Result.ToList();
                return View(viewModel);
            }

            var forumThread = new ForumThread
            {
                Title = viewModel.Title,
                ThreadCreator = account,
                SecondCategoryId = viewModel.SecondCategoryId,  // Här har vi lagt till .Value korrekt
                CreatedAt = DateTime.Now
            };

            await _forumThreadRepository.AddAsync(forumThread);
            await _forumThreadRepository.SaveChanges();

            // Skapa första posten direkt kopplad till tråden!
            var threadPost = new ThreadPost
            {
                PostMessage = viewModel.ThreadPost.PostMessage, // Här använder vi det rätta namnet
                PostDate = DateTime.Now,
                PostCreator = account,
                //ForumThread = forumThread,
                ForumThreadId = forumThread.ForumThreadID
            };

            await _threadPostRepository.AddAsync(threadPost);
            await _threadPostRepository.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var forumThread = await _forumThreadRepository.GetByIDAsync(id);
            if (forumThread == null)
            {
                return NotFound();
            }

            return View(forumThread);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var forumThread = await _forumThreadRepository.GetByIDAsync(id);
            if (forumThread == null)
            {
                return NotFound();
            }

            await _forumThreadRepository.DeleteAsync(forumThread);
            return RedirectToAction(nameof(Index));
        }

    }
}
