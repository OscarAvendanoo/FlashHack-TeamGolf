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

        public IActionResult Index()
        {
            return View();
        }

        // POST: AuthController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserLoginViewModel userLoginVM)
        {
            if (ModelState.IsValid)
            {
                var user = (await userRepository.GetAllAsync()).FirstOrDefault(c => c.Email == userLoginVM.Email && c.Password == userLoginVM.Password);
                {
                    ViewData["Message"] = "Invalid user id or password.";
                    return View(userLoginVM);
                }

                // Set Session variables
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("ProfileIMG", user.ProfileImage);

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
