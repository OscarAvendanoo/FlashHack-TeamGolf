using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        // Det är en Mall Controller går justera respectiva methoder

        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/ Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel registerVM)
        {
            if (ModelState.IsValid)
            {

               await _unitOfWork.BeginTransactionAsync(); //Starting the transaction here
                try
                {
                    var user = new User
                    {
                        FirstName = registerVM.FirstName,
                        LastName = registerVM.LastName,
                        Email = registerVM.Email,
                        UserName = registerVM.UserName,
                        Password = registerVM.Password

                    };
                    await _unitOfWork.UserRepository.AddAsync(user);

                    var account = new Account
                    {
                        Biography = registerVM.Biography,
                        PhoneNumber = registerVM.PhoneNumber,
                        DisplayName = registerVM.DisplayName,
                        IsPremium = registerVM.IsPremium,
                        UserId = user.UserId,
                        User = user,
                        AccountCreatedAt = DateTime.UtcNow
                    };
                    await _unitOfWork.AccountRepository.AddAsync(account);
                    await _unitOfWork.CommitTransactionAsync(); // Commit the transaction if Successful

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

                    //return RedirectToAction("Index","Auth");
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(); // Rolling back whole transaction if error occured
                    ModelState.AddModelError("", "Registrering mislyckades. Försök igen.");
                }

            }
            return View(registerVM);

        }
    }
}
