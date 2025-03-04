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
        private readonly ISecondCategoryRepository secondCategoryRepository;
        private readonly IForumThreadRepository forumThreadRepository;

        public AllForumThreadViewComponent(IForumThreadRepository forumThreadRepository)
        {
            this.secondCategoryRepository = secondCategoryRepository;
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
                            //.OrderByDescending(t=>t.CreatedAt)
                            //.Take(1)
                            //.ToList()
                        })
                        .ToList()
                })
                .ToList(); 

            

            return View(forumThreadVM);
        }


        //public async Task<IViewComponentResult> InvokeAsync(int? page)
        //{
        //    // Hämtar alla forumtrådar inklusive inlägg och skapare
        //    var forumThreads = await forumThreadRepository.GetAllIncludePostsAndCreators();

        //    // Gruppera först trådarna efter huvudkategori
        //    var groupedThreads = forumThreads
        //        .GroupBy(t => t.SecondCategory.MainCategory.Name) // Gruppera efter huvudkategori
        //        .ToList();

        //    var forumThreadVM = groupedThreads.Select(mainGroup => new ForumThreadPostVM
        //    {
        //        MainCategoryName = mainGroup.Key, // Namn på huvudkategori
        //        SecondCategories = mainGroup
        //            .GroupBy(t => t.SecondCategory.Name) // Gruppera inom huvudkategorin efter subkategori
        //            .Select(subGroup => new SubCategoryVM
        //            {
        //                SecondCategoryName = subGroup.Key, // Namn på subkategori
        //                ForumThreads = subGroup.ToList() // Lista av trådar i subkategorin
        //            })
        //            .ToList()
        //    }).ToList();

        //    return View(forumThreadVM);
        //}

        //public async Task<IViewComponentResult> InvokeAsync(int? page)
        //{


        //    // Hämtar alla forumtrådar
        //    var forumThreads = await forumThreadRepository.GetAllIncludePostsAndCreators();

        //    // Gruppera trådarna efter huvudkategori och lista alla underkategorier (SecondCategory)
        //    var groupedThreads = forumThreads
        //        .GroupBy(t => t.SecondCategory.MainCategory.Name)
        //        .ToList();

        //    var forumThreadVM = groupedThreads.Select(g => new ForumThreadPostVM
        //    {
        //        MainCategoryName = g.Key,
        //        ForumThreads = g.ToList()
        //    }).ToList();



        //    return View(forumThreadVM);
        //}

    }
}

