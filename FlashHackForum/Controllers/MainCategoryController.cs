using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlashHackForum.Data;
using FlashHackForum.Models;
using FlashHackForum.Data.Interfaces;

namespace FlashHackForum.Controllers
{
    public class MainCategoryController : Controller
    {
        private readonly IMainCategoryRepository mainCategoryRepository;
        public MainCategoryController(IMainCategoryRepository mainCategoryRepository)
        {
            this.mainCategoryRepository = mainCategoryRepository;
        }


        /* 
         * Detta stycke kan vi eventuellt använda för att kontrollera om användare är admin?
          
        private bool IsAdmin()
        {
            // Här kollar vi om sessionen innehåller en "Role" med värdet "Admin"
            return HttpContext.Session.GetString("Role") == "Admin";
        }


        Samt denna i Get-metoderna: 

        if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Home");
            }


        */


        // GET: MainCategory/GetAll
        public async Task<ActionResult> GetAllCategories()
        {
            var mainCategories = await mainCategoryRepository.GetAllAsync();
            return View(mainCategories);
        }

        // GET: MainCategory/Create
        public ActionResult CreateCategory()
        {
            return View();
        }

        // POST: MainCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(int categoryId, MainCategory mainCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await mainCategoryRepository.AddAsync(mainCategory);
                    return RedirectToAction("GetAllCategories");
                }
                catch
                {
                    // Om det händer ngt fel kan vi lägga till logik för att hantera det här
                    return View();
                }
            }
            return View(mainCategory);
        }

        // GET: MainCategory/Edit/5
        public async Task<ActionResult> EditCategory(int id)
        {
            var mainCategory = await mainCategoryRepository.GetByIDAsync(id);
            if (mainCategory == null)
            {
                return NotFound();  // Hantera fall där kategorin inte finns
            }
            return View(mainCategory);
        }

        // POST: MainCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(int id, MainCategory mainCategory)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    await mainCategoryRepository.UpdateAsync(mainCategory);
                    return RedirectToAction("GetAllCategories");
                }
                catch
                {
                    
                    return View();
                }
            }
            return View(mainCategory);
        }

        // GET: MainCategory/Delete/5
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var mainCategory = await mainCategoryRepository.GetByIDAsync(id);
            if (mainCategory == null)
            {
                return NotFound();  // Hantera fall där kategorin inte finns
            }
            return View(mainCategory);
        }

        // POST: MainCategory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCategory(int id, MainCategory mainCategory)
        {
            try
            {
                await mainCategoryRepository.DeleteAsync(mainCategory);
                return RedirectToAction("GetAllCategories");
            }
            catch
            {
                // Lägg till logik för att hantera fel
                return View();
            }
        }
    }
}
