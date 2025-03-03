using FlashHackForum.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashHackForum.Data;
using FlashHackForum.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext dbContext;

        public CategoryMenuViewComponent(ApplicationDbContext dbContext)
        {
            
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mainCategories = await dbContext.MainCategories.Include(mc=>mc.SecondCategories).ToListAsync();
            return View(mainCategories);
        }
    }
}
