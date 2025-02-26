using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FlashHackForum.Controllers
{

    
    public class SecondCategoryController : Controller
    {
        private readonly ISecondCategoryRepository secondCategoryRepository;
        private readonly IMainCategoryRepository mainCategoryRepository;

        public SecondCategoryController(ISecondCategoryRepository secondCategoryRepository, IMainCategoryRepository mainCategoryRepository)
        {
            this.secondCategoryRepository = secondCategoryRepository;
            this.mainCategoryRepository = mainCategoryRepository;
        }


        //GET: SecondCategory/GetAllCategories
        public async Task<ActionResult> GetAllCategories()
        {
            return View(await secondCategoryRepository.GetAllAsync());
        }
        

        
        
        /*Här lurade jag runt lite och försökte komma på något vettigt med validering och skapade en viewmodel för både CREATE och EDIT.
         Blir lite av spaghettikod och kan nog förbättra det men just nu funkar det :)*/

        
        // GET: CategoryController/CreateCategory
        public async Task<ActionResult> CreateCategory()
        {
            var model = new CreateEditSubCategoryViewModel
            {
                MainCategoryId = null,
                MainCategories = await mainCategoryRepository.GetAllAsync()
            };
            return View(model);
        }

        // POST: CategoryController/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(CreateEditSubCategoryViewModel createSubCategoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    createSubCategoryViewModel.MainCategories = await mainCategoryRepository.GetAllAsync();
                    return View(createSubCategoryViewModel);
                }
                SecondCategory secondCategory = new SecondCategory
                {

                    Name = createSubCategoryViewModel.Name,
                    MainCategoryId = (int)createSubCategoryViewModel.MainCategoryId,

                };
                await secondCategoryRepository.AddAsync(secondCategory);
                return RedirectToAction("GetAllCategories");

            }
            catch
            {
                return NotFound();
            }
        }

        // GET: CategoryController/EditCategory/5
        public async Task<ActionResult> EditCategory(int id)
        {
            var model = new CreateEditSubCategoryViewModel
            {
                MainCategoryId = null,
                MainCategories = await mainCategoryRepository.GetAllAsync()
            };
            var subCategory = await secondCategoryRepository.GetByIDAsync(id);
            
            
            ViewBag.SubCategoryName = subCategory.Name;
            return View(model);
            
        }

        // POST: CategoryController/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(int id, CreateEditSubCategoryViewModel createSubCategoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    createSubCategoryViewModel.MainCategories = await mainCategoryRepository.GetAllAsync();
                    return View(createSubCategoryViewModel);
                }
                
                var secondCategory = await secondCategoryRepository.GetByIDAsync(id);
                if (secondCategory != null)
                {
                    secondCategory.MainCategoryId = (int)createSubCategoryViewModel.MainCategoryId;
                    secondCategory.Name = createSubCategoryViewModel.Name;

                }
                await secondCategoryRepository.UpdateAsync(secondCategory);
                return RedirectToAction("GetAllCategories");
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
