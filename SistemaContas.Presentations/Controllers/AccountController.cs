
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentations.Models;
using System.Security.Claims;

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
                        //armazenando os dados do usuário autenticado
                        var userModel = new UserModel();
                        
                        userModel.Email = usuario.Email;
                        userModel.IdUser = usuario.IdUser;
                        userModel.Name = usuario.Name;
                        userModel.DateTimeAcess = DateTime.Now;

                        //serializar para json para armazenar os dados no arquivo de cookie
                        var json = JsonConvert.SerializeObject(userModel);

                        //criar a identificação do usuário autenticado para ser gravada no cookie de autenticação
                        var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, json) //Identificação do usuário
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravando a identificação no cookie de autorização.
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
