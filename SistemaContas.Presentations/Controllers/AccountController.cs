﻿
using Microsoft.AspNetCore.Mvc;

namespace SistemaContas.Presentations.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Login()
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
