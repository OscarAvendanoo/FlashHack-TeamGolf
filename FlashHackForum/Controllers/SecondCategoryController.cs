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


        //SECONDCATEGORY
        // GET: CategoryController/Details/5
        public ActionResult GetSecondCategory(int id)
        {
            return View(secondCategoryRepository.GetByCategoryIdIncludeThreads(id));
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
        public async Task<ActionResult> EditSecondCategory(int id)
        {
            return View(await secondCategoryRepository.GetByIDAsync(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSecondCategory(int id, SecondCategory secondCategory)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
