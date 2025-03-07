using FlashHackForum.Data;
using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashHackForum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<Competens> _competenseRepository; // endast för mockdata. radera senare

        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository, IRepository<Competens> competenseRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
            _competenseRepository = competenseRepository; // endast för mockdata. radera senare
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
                        
                        PhoneNumber = registerVM.PhoneNumber,
                        Email = registerVM.AccountEmail,
                        DisplayName = registerVM.DisplayName,
                        IsPremium = registerVM.IsPremium,
                        UserId = user.UserId,
                        User = user,
                        Employer = registerVM.Employer,
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
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransactionAsync(); // Rolling back whole transaction if error occured
                    ModelState.AddModelError("", "Registrering mislyckades. Försök igen.");
                }

            }
            return View(registerVM);

        }



        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIDAsync(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Användare finns inte.";
                return NotFound();
            }

            user.Account = await _accountRepository.GetAccountByUserID(user.UserId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Kontot finns inte.";
                return NotFound();
            }

            // mockad data för att lägga till kompetenser i profilsidan. Radera senare när
            // funktionaliteten finns för att lägga till sådana till kontot.
            var getCompetenses = await _competenseRepository.GetAllAsync();
            var mockCompetences = new List<UserCompetence>
            {
                new UserCompetence { Id = 1, Account = user.Account, CompetensId = 1, Competens= getCompetenses.FirstOrDefault(c => c.CompetensId == 1),Grade = 2, Education = null },
                new UserCompetence { Id = 2, Account = user.Account, CompetensId = 2, Competens= getCompetenses.FirstOrDefault(c => c.CompetensId == 2),Grade = 1, Education = null },
                new UserCompetence { Id = 3, Account = user.Account, CompetensId = 3, Competens= getCompetenses.FirstOrDefault(c => c.CompetensId == 3),Grade = 4, Education = null },
                new UserCompetence { Id = 4, Account = user.Account, CompetensId = 4, Competens= getCompetenses.FirstOrDefault(c => c.CompetensId == 4),Grade = 3, Education = null },
            };
            user.Account.UserCompetences = mockCompetences;

            return View(user);
        }

       
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                // Hämta användaren och kontot från databasen
                var existingUser = await _userRepository.GetByIDAsync(user.UserId);
                var existingAccount = await _unitOfWork.AccountRepository.GetByIDAsync((int)user.AccountId!);
                               
                if (existingUser == null || existingAccount == null)
                {
                    TempData["ErrorMessage"] = "Användare eller användarkonto finns inte.";
                    return NotFound();
                }

                // Uppdatera användardata
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;

                // Uppdatera kontoinformation
                existingAccount.DisplayName = user.Account.DisplayName;
                existingAccount.PhoneNumber = user.Account.PhoneNumber;
                existingAccount.Email = user.Account.Email;
                existingAccount.Employer = user.Account.Employer;

                existingAccount.Biography = user.Account.Biography;
                existingAccount.Signature = user.Account.Signature;

                existingAccount.ShowAdvertisements = user.Account.ShowAdvertisements;
                existingAccount.ShowContact = user.Account.ShowContact;
                existingAccount.ShowToCompanies = user.Account.ShowToCompanies;

                // Spara uppdateringarna
                await _userRepository.UpdateAsync(existingUser);
                await _unitOfWork.AccountRepository.UpdateAsync(existingAccount);

                // Redirect till Details för att visa de uppdaterade uppgifterna
                return RedirectToAction("Details", new { id = user.UserId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Det gick inte att uppdatera användardata.");
            }
            return View("Details", user);  // Om validering misslyckades, visa om användardatan i Details igen
        }
    }
}
