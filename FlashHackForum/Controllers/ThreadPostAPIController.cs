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
    public class ThreadPostAPIController : Controller
    {
        private readonly IThreadPostRepository _threadPostRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IForumThreadRepository _forumThreadRepository;
        private readonly IUserPostReaction _userPostReactionRepository;

        public ThreadPostAPIController(IThreadPostRepository threadPostRepository,
                                    IAccountRepository accountRepository,
                                    IForumThreadRepository forumThreadRepository,
                                    IUserPostReaction userPostReactionRepository)
        {
            _threadPostRepository = threadPostRepository;
            _accountRepository = accountRepository;
            _forumThreadRepository = forumThreadRepository;
            _userPostReactionRepository = userPostReactionRepository;
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
