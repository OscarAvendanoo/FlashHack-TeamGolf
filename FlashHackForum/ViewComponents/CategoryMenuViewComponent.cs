using FlashHackForum.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashHackForum.Data;

namespace FlashHackForum.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IMainCategoryRepository _mainCategoryRepository;

        public CategoryMenuViewComponent(IMainCategoryRepository mainCategoryRepository)
        {
            _mainCategoryRepository = mainCategoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mainCategories = await _mainCategoryRepository.GetAllAsync();
            return View(mainCategories);
        }
    }
}
