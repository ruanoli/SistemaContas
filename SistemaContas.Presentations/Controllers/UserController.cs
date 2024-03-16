using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Messages.Models;
using SistemaContas.Messages.Services;
using SistemaContas.Presentations.Models;
using System.Reflection;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult MyData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Mydata(ChangePasswordModel changeModel)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var userRepo = new UserRepository();
                    var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);
                    if (user != null)
                    {
                        

                        TempData["Message"] = "Senha Alterada com sucesso. Verifique sua caixa de e-mail";
                        ModelState.Clear();

                    }
                    else
                    {
                        TempData["MessageAlert"] = "Usuário não encontrado";
                    }
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = "Falha ao Recuperar Senha!";

                }
            }
            return View();
        }
    }
}
