
using Microsoft.AspNetCore.Mvc;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
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

            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UserRepository();
                    var usuario = usuarioRepository.GetByEmailAndPassword(model.Email, model.Password);
                    if (usuario != null)
                    {
                        //Redireciona para a página/método index do controlador Dashboard.
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["Message"] = "E-mail e/ou senha inválidos.";
                    }
                }
                catch
                {
                    TempData["Message"] = "Falha ao autenticar usuário";
                }
            }


            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model)
        {
            //Verifica se tem algum erro nos dados recebidos,
            //com isso não será possível salvar dados sem ser validados no banco.
            if (ModelState.IsValid)
            {
                try
                {
                    var userRepository = new UserRepository();
                    var usuario = new UserRegister();

                    if (userRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["ErrorEmail"] = "E-mail informado já existe no sistena, tente outro.";
                    }
                    else
                    {
                        usuario.Email = model.Email;
                        usuario.IdUser = Guid.NewGuid();
                        usuario.Name = model.Name;
                        usuario.Password = model.Password;
                        usuario.DateTimeCreation = DateTime.Now;
                    }

                    userRepository.Insert(usuario);

                    //Tempdata é um dicionário (Armazena chave/valor).
                    //Retorna uma mensagem para a página (View)
                    //TempData["Message"] = "Usuário cadastrado!";

                    //Limpa os campos no formulário depois de cadastra-los.
                    ModelState.Clear();
                    return RedirectToAction("Login");

                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Falha ao cadastrar usuário!";
                }
            }
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
