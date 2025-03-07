using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Data.SqlTypes;

namespace FlashHackForum.Controllers
{
    [ApiController]
    [Route("api/threadposts")]
    public class ThreadPostController : Controller
    {
        private readonly IThreadPostRepository _threadPostRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IUserPostReaction _userPostReactionRepository;

        public ThreadPostController(IThreadPostRepository threadPostRepository,
                                    IAccountRepository accountRepository,
                                    IForumThreadRepository forumThreadRepository,
                                    IUserPostReaction userPostReactionRepository)
        {
            _threadPostRepository = threadPostRepository;
            _accountRepository = accountRepository;
            _forumThreadRepository = forumThreadRepository;
            _userPostReactionRepository = userPostReactionRepository;
        }

        public async Task<ActionResult> CreateReplyPost(int postId, int threadId)
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


        // Endpoint to Like a ThreadPost
        [HttpPost("{PostId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();
            var post = await _threadPostRepository.GetByIDAsync(postId);

            if (post == null) return NotFound();

            var existingReaction = await _userPostReactionRepository.GetUserPostReaction((int)userId, post.ThreadPostId);
            if (existingReaction != null)
            {
                if (existingReaction.ReactionType == ReactionType.Like)
                {
                    existingReaction.ReactionType = ReactionType.None;
                    post.LikeCount--;
                    await _threadPostRepository.SaveChanges();

                    //return Ok(new { message = "Already liked." });
                }
                else if (existingReaction.ReactionType == ReactionType.None)
                {
                    existingReaction.ReactionType = ReactionType.Like;
                    post.LikeCount++;
                    await _threadPostRepository.SaveChanges();
                }
                else
                
                {
                    existingReaction.ReactionType = ReactionType.Like;
                    post.DislikeCount--;
                    post.LikeCount++;
                    await _threadPostRepository.SaveChanges();
                }
            }
            else
            {
                var reaction = new UserPostReaction
                {
                    ThreadPostId = postId,
                    UserId = (int)userId,
                    ReactionType = ReactionType.Like
                };
                await _userPostReactionRepository.AddNewPostReaction(reaction);
                post.LikeCount++;
                await _threadPostRepository.SaveChanges();
            }
            return Ok(new { LikeCount = post.LikeCount, DislikeCount = post.DislikeCount });
        }

        // Endpoint to Dislike a ThreadPost
        [HttpPost("{PostId}/dislike")]
        public async Task<IActionResult> DislikePost(int postId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();
            var post = await _threadPostRepository.GetByIDAsync(postId);
            if (post == null) return NotFound();
            var existingReaction = await _userPostReactionRepository.GetUserPostReaction((int)userId, postId);
            if (existingReaction != null)
            {
                if (existingReaction.ReactionType == ReactionType.Dislike)
                {
                    existingReaction.ReactionType = ReactionType.None;
                    post.DislikeCount--;
                    await _threadPostRepository.SaveChanges();
                }
                else if (existingReaction.ReactionType == ReactionType.None)
                {
                    existingReaction.ReactionType = ReactionType.Dislike;
                    post.DislikeCount++;
                    await _threadPostRepository.SaveChanges();
                }
                else
                {
                    existingReaction.ReactionType = ReactionType.Dislike;
                    post.DislikeCount++;
                    post.LikeCount--;
                    await _threadPostRepository.SaveChanges();
                }
            }
            else
            {
                var reaction = new UserPostReaction
                {
                    UserId = (int)userId,
                    ThreadPostId = postId,
                    ReactionType = ReactionType.Dislike
                };
                await _userPostReactionRepository.AddNewPostReaction(reaction);
                post.DislikeCount++;
                await _threadPostRepository.SaveChanges();
            }
            return Ok(new { post.LikeCount, post.DislikeCount });

        }

    }
}
