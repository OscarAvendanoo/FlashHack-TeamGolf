using FlashHackForum.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashHackForum.Models;
using Microsoft.AspNetCore.Http;
using FlashHackForum.ViewModels;
using FlashHackForum.Data;

namespace FlashHackForum.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepo;

        public AuthController(IUserRepository userRepository)
        {
            this._userRepo = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: AuthController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginViewModel userLoginVM)
        {
            if (ModelState.IsValid)
            {
                var user = (await _userRepo.GetAllAsync()).FirstOrDefault(c => c.Email == userLoginVM.Email && c.Password == userLoginVM.Password);

                if (user == null)
                {
                    ViewData["Message"] = "Invalid user id or password.";
                   return View(userLoginVM);
                }

                // Set Session variables
                HttpContext.Session.SetInt32("UserId", user.UserId);
                //HttpContext.Session.SetString("UserName", ($"{user.FirstName} {user.LastName}"));
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
