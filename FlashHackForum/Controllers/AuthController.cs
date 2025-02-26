using FlashHackForum.Data.Interfaces;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // Get 
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Index(UserLoginViewModel userLoginVM)
        {
            if (ModelState.IsValid) 
            {
                var user = (await userRepository.GetAllAsync()).FirstOrDefault(c => c.Email == userLoginVM.Email && c.Password == userLoginVM.Password);
                if (user == null)
                {
                    ViewData["Message"] = "Fel epost eller lösenord.";
                    return View(userLoginVM);
                }

                // Set Session variables
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);

                ViewBag.UserName = user.UserName;

                // If user IS an admin, set Session int [IsAdmin] to 1
                if (user.IsAdmin)
                {
                    HttpContext.Session.SetInt32("IsAdmin", 1);
                }
                return RedirectToAction("Index", "Home");

            }
            return View(userLoginVM);

        }
        
    }
}
