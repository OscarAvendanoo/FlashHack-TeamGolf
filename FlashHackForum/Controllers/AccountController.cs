using FlashHackForum.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
    }
}
