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

        // GET: ThreadController/Delete/5
        public async Task<ActionResult> DeleteByOwner(int id)
        {
            var threadToDelete = await _forumThreadRepository.GetByIDAsync(id);
            var deleteThreadVM = new DeleteThreadVM();
            deleteThreadVM.ThreadId = id;
            deleteThreadVM.ThreadName = threadToDelete.Title;

            return View(deleteThreadVM);
        }

        // POST: ThreadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteByOwner(DeleteThreadVM deleteThreadVM)
        {
            var threadToDelete = await _forumThreadRepository.GetByIdIncludePostsAndCreators(deleteThreadVM.ThreadId);
            if (threadToDelete == null)
            {
                ModelState.AddModelError("", "The thread could not be found");
                return View(deleteThreadVM);
            }
            var user = await _userRepository.GetUserByUsername(HttpContext.Session.GetString("UserName"));
            var userAccount = await _accountRepository.GetAccountByUserID(user.UserId);
            if (threadToDelete.ThreadCreator != userAccount)
            {
                ModelState.AddModelError("", "You can not delete a thread that does not belong to you.");
                return View(deleteThreadVM);
            }
            if (threadToDelete.PostsInThread.Count > 1)
            {
                ModelState.AddModelError("", "You can not delete a thread that has replies.");
                return View(deleteThreadVM);
            }

            await _forumThreadRepository.DeleteAsync(threadToDelete);

            return RedirectToAction("Index");
        }

        // GET: ThreadController/Edit/5
        public async Task<ActionResult> EditByOwner(int id)
        {
            var editThreadVM = new EditThreadVM();
            var threadToEdit = await _forumThreadRepository.GetByIdIncludePostsAndCreators(id);
            editThreadVM.FirstPostMessage = threadToEdit.PostsInThread.OrderBy(p => p.PostDate).FirstOrDefault().PostMessage;
            editThreadVM.Desrciption = threadToEdit.Title;
            editThreadVM.IsAnonymous = threadToEdit.IsAnonymous;
            editThreadVM.ThreadToEditId = id;

            var user = await _userRepository.GetUserByUsername(HttpContext.Session.GetString("UserName"));
            if (user.IsAdmin == false)
            {
     
                var userAccount = await _accountRepository.GetAccountByUserID(user.UserId);
                if (threadToEdit.ThreadCreator != userAccount)
                {
                    return NotFound();
                }


                return View(editThreadVM);
            }

            return View(editThreadVM);

        }

        // POST: ThreadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditByOwner(EditThreadVM editThreadVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "All fields need to be filled.");
                return View(editThreadVM);
            }

            var threadToEdit = await _forumThreadRepository.GetByIdIncludePostsAndCreators(editThreadVM.ThreadToEditId);

            if (threadToEdit.PostsInThread.Count > 1)
            {
                ModelState.AddModelError(string.Empty, "You cannot edit this thread because others have replied.");
                return View();
            }

            threadToEdit.Title = editThreadVM.Desrciption;
            threadToEdit.IsAnonymous = editThreadVM.IsAnonymous;

            //await _threadRepository.UpdateAsync(threadToEdit);
            await _threadPostRepository.SaveChanges();

            var firstPostInThread = threadToEdit.PostsInThread.OrderBy(p => p.PostDate).FirstOrDefault();
            firstPostInThread.PostMessage = editThreadVM.FirstPostMessage;

            //await _threadPostRepository.UpdateAsync(firstPostInThread);
            await _threadPostRepository.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
