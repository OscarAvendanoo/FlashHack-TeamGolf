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
        public async Task< IActionResult> Index(UserLoginViewModel userLoginVM)
        {
            if (ModelState.IsValid) 
            {
                var user = (await userRepository.GetAllAsync()).FirstOrDefault(c => c.Email == userLoginVM.Email && c.Password == userLoginVM.Password);
                if (user == null) 
                {
                    ViewBag.Error = "Invalid Email or Password.";
                    ViewData["Message"] = "Invalid user id or password.";
                    return View(userLoginVM);
                }
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", ($"{user.FirstName} {user.LastName}"));
                return RedirectToAction("index","Home");

            }
            return View(userLoginVM);

        }
    }
}
