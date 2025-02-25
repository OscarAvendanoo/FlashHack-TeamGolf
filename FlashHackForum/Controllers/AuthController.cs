using Microsoft.AspNetCore.Mvc;

namespace FlashHackForum.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
          
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
