using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FlashHackForum.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlashHackForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRepository<Competens> _competensRepository;

        public AccountController(IAccountRepository accountRepository, IRepository<Competens> competensRepository )
        {
            _accountRepository = accountRepository;
            _competensRepository = competensRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpdateSignature()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var account = await _accountRepository.GetAccountByUserID((int)userId);

            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSignature(string signature)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var account = await _accountRepository.GetAccountByUserID((int)userId);
            
            if (account != null)
            {
               account.Signature = signature;
                await _accountRepository.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddCompetences(int id)
        {
            var account = await _accountRepository.GetByIDAsync(id);

            var getCompetenses = await _competensRepository.GetAllAsync();
            
            var viewModel = new AddCompetenceViewModel
            {
                AccountId = id,
                Account = account,
                Competenses = getCompetenses,
                UserCompetences = new List<UserCompetence>()
            };
            
            return View(viewModel);
        }
    }
}

