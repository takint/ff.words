using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ff.words.pages.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
