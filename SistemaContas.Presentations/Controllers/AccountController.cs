
using Microsoft.AspNetCore.Mvc;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// Método usado somente para abrir a página view.
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método executando quando o botão SUBMIT (POST) do formuláriona view é clicado. 
        /// Com isso os dados são passados na model dentro da view e os dados são
        /// enviados para o backend (controller) armazenar no banco.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model)
        {
            return View();
        }
    }
}
