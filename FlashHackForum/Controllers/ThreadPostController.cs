using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class ThreadPostController : Controller
    {
        private readonly IThreadPostRepository _threadPostRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IForumThreadRepository _forumThreadRepository;
        public ThreadPostController(IThreadPostRepository threadPostRepository,IAccountRepository accountRepository, IForumThreadRepository forumThreadRepository )
        {
            _threadPostRepository = threadPostRepository;
            _accountRepository = accountRepository;
            _forumThreadRepository = forumThreadRepository;
        }

        public async Task<ActionResult> CreateReplyPost(int postId,int threadId)
        {
            var postToReplyTo = await _threadPostRepository.GetPostByIDIncludePostCreator(postId);
            var createPostReplyVM = new CreatePostReplyVM();
            createPostReplyVM.PostToReplyTo = postToReplyTo;
            createPostReplyVM.ThreadId = threadId;
            return View(createPostReplyVM);
        }

        // POST: ThreadPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReplyPost(CreatePostReplyVM createPostReplyVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Du måste fylla i ett svar.");
            }
            var thread = await _forumThreadRepository.GetByIDAsync(createPostReplyVM.ThreadId);
            var userID = HttpContext.Session.GetInt32("UserId");
            var userAccount = await _accountRepository.GetAccountByUserID(userID.Value); 
            var newPostWithReply = new ThreadPost
            {
                PostMessage = createPostReplyVM.PostMessage,
                ReplyToPostId = createPostReplyVM.PostToReplyTo.ThreadPostId,
                ForumThread = thread,
                PostCreatorId = userAccount.AccountId
            };

            await _threadPostRepository.AddAsync(newPostWithReply);

            return RedirectToAction("ShowThread", "ForumThread", new { id = createPostReplyVM.ThreadId });

        }
        public async Task<ActionResult> CreatePost(int threadId)
        {
            var thread = await _forumThreadRepository.GetByIdIncludePostsAndCreators(threadId);
            var firstPostInThread = thread.PostsInThread.OrderBy(p => p.PostDate).FirstOrDefault();
            var createPostVM = new CreatePostVM(); 
            createPostVM.ThreadId = threadId;
            return View(createPostVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(CreatePostVM createPostVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Du måste skriva en meddelande för att kunna svara på tråden.");
                return View(createPostVM);
            }
            var userId = HttpContext.Session.GetInt32("UserId");
            var account = await _accountRepository.GetAccountByUserID(userId.Value);
            var newPost = new ThreadPost();
            newPost.PostMessage = createPostVM.PostMessage;
            newPost.PostCreatorId = account.AccountId;
            newPost.ForumThreadId = createPostVM.ThreadId;

            await _threadPostRepository.AddAsync(newPost);

            return RedirectToAction("ShowThread", "ForumThread", new { id = createPostVM.ThreadId });
        }


    }
}
