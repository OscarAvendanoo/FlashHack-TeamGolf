using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FlashHackForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMainCategoryRepository mainCategoryRepository;
        private readonly ISecondCategoryRepository secondCategoryRepository;

        public HomeController(ILogger<HomeController> logger, IMainCategoryRepository mainCategoryRepository, ISecondCategoryRepository secondCategoryRepository)
        {
            _logger = logger;
            this.mainCategoryRepository = mainCategoryRepository;
            this.secondCategoryRepository = secondCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var mainCategories = await mainCategoryRepository.GetAllAsync();
            ViewBag.MainCategories = mainCategories;
            var subCateogory = await secondCategoryRepository.GetAllAsync();
            ViewBag.SecondCategory = subCateogory;



            return View();
        }

        public async Task<IActionResult> Privacy()
        {
          
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //Här kommer en kommentar om du vill
    }
}
