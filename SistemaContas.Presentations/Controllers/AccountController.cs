
using Microsoft.AspNetCore.Mvc;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }
    }
}
