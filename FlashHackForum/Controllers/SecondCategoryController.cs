using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using Microsoft.AspNetCore.Mvc;


namespace FlashHackForum.Controllers
{

    
    public class SecondCategoryController : Controller
    {
        private readonly ISecondCategoryRepository secondCategoryRepository;

        public SecondCategoryController(ISecondCategoryRepository secondCategoryRepository)
        {
            this.secondCategoryRepository = secondCategoryRepository;
        }


        //GET: SecondCategory/GetAllCategories
        public async Task<ActionResult> GetAllCategories()
        {
            return View(await secondCategoryRepository.GetAllAsync());
        }
        

        // GET: CategoryController/CreateCategory
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: CategoryController/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(int categoryId, SecondCategory secondCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await secondCategoryRepository.AddAsync(secondCategory);
                    return RedirectToAction("GetAllCategories");
                }
                return View();
                
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: CategoryController/EditCategory/5
        public async Task<ActionResult> EditCategory(int id)
        {
            return View(await secondCategoryRepository.GetByIDAsync(id));
        }

        // POST: CategoryController/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(int id, SecondCategory secondCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await secondCategoryRepository.UpdateAsync(secondCategory);
                    return RedirectToAction("GetAllCategories");
                }
                return View();
                
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: CategoryController/DeleteCategory/5
        public async Task<ActionResult> DeleteCategory(int id)
        {
            return View(await secondCategoryRepository.GetByIDAsync(id));
        }

        // POST: CategoryController/DeleteCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(int id, SecondCategory secondCategory)
        {
            try
            {
                await secondCategoryRepository.DeleteAsync(secondCategory);
                return RedirectToAction("GetAllCategories");
            }
            catch
            {
                return View();
            }
        }
    }
}
