using FlashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Extensions;

namespace FlashHackForum.ViewComponents
{
    public class AllForumThreadViewComponent : ViewComponent
    {
       
        private readonly IForumThreadRepository forumThreadRepository;

        public AllForumThreadViewComponent(IForumThreadRepository forumThreadRepository)
        {
            this.forumThreadRepository = forumThreadRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? page)
        {
            // Hämtar alla forumtrådar inklusive inlägg och skapare
            var forumThreads = await forumThreadRepository.GetAllIncludePostsAndCreators();

            // Gruppera först trådarna efter huvudkategori
            var forumThreadVM = forumThreads
                .GroupBy(t => t.SecondCategory.MainCategory.Name)
                .Select(mainGroup => new ForumThreadPostVM
                {
                    MainCategoryName = mainGroup.Key, // Namn på huvudkategori
                    SecondCategories = mainGroup
                        .GroupBy(t => t.SecondCategory.Name)
                        .Select(subGroup => new SubCategoryVM
                        {
                            SecondCategoryName = subGroup.Key,
                            ForumThreads = subGroup.ToList(),
                            TotalThreads = subGroup.Count(),
                            LatestThread = subGroup.OrderByDescending(t => t.CreatedAt).FirstOrDefault()
                            
                        })
                        .ToList()
                })
                .ToList(); 

            

            return View(forumThreadVM);
        }


        
    }
}

