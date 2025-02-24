using FlashHackForum.Data;
using FlashHackForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FlashHackForum.Controllers
{
    public class SecondCategoryController : Controller
    {
        private readonly MainCategoryRepository mainCategoryRepo;
        private readonly SecondCategoryRepository secondCategoryRepository;

        public SecondCategoryController(MainCategoryRepository mainCategoryRepo, SecondCategoryRepository secondCategoryRepository)
        {
            this.mainCategoryRepo = mainCategoryRepo;
            this.secondCategoryRepository = secondCategoryRepository;
        }


        
        
        //GET: SecondCategory/GetAll
        public async Task<ActionResult> GetAllSecondCategoriesAsync()
        {
            return View(await secondCategoryRepository.GetAllAsync());
        }
        
        // GET: CategoryController/Details/5
        public async Task<ActionResult> GetSecondCategoryAsync(int id)
        {
            return View(await secondCategoryRepository.GetByCategoryIdIncludeThreads(id));
        }

        // GET: CategoryController/Create
        public ActionResult CreateSecondCategory()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSecondCategoryAsync(int categoryId, SecondCategory secondCategory)
        {
            try
            {
                await secondCategoryRepository.AddAsync(secondCategory);
                return View(secondCategory);
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> EditSecondCategoryAsync(int id)
        {
            return View(await secondCategoryRepository.GetByIDAsync(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSecondCategoryAsync(int id, SecondCategory secondCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(await secondCategoryRepository.UpdateAsync(secondCategory));
                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> DeleteSecondCategoryAsync(int id)
        {
            return View(await secondCategoryRepository.GetByIDAsync(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSecondCategoryAsync(int id, SecondCategory secondCategory)
        {
            try
            {
                await secondCategoryRepository.DeleteAsync(secondCategory);
                return View(secondCategory);
            }
            catch
            {
                return View();
            }
        }
    }
}
