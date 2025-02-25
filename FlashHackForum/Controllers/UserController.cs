using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class UserController : Controller
    {
        // GET: AccountController/ Logout
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
