
using Bogus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Messages.Models;
using SistemaContas.Messages.Services;
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

                        //autentica o usuário.
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                        //Redireciona para a página/método index do controlador Dashboard.
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        TempData["MessageAlert"] = "E-mail e/ou senha inválidos.";
                    }
                }
                catch
                {
                    TempData["MessageError"] = "Falha ao autenticar usuário";
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
                    TempData["MessageError"] = "Falha ao cadastrar usuário!";
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
            if(ModelState.IsValid)
            {
                try
                {
                    var userRepo = new UserRepository();
                    var user = userRepo.GetByEmail(model.Email);

                    //var userLogado = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);


                    if(user != null)
                    {
                        //Gera uma nova senha com 10 digitos utilizando a biblioteca Bogus.
                        //A senha é gerada com letras maiúsculas e minusculas e com números, o @ interpolado é para forçarmos um ccaractere especial no final da senha.
                        var newPassword = new Faker().Internet.Password(10) + "@";

                        //criando o conteúdo do email.
                        var emailModel = new EmailMessageModel();

                        emailModel.EmailReceiver = user.Email;
                        emailModel.Message = $@"
                            <h4>Olá, <strong>{user.Name}</strong>! Uma nova senha foi gerada com sucesso. </h4>
                            <p>Acesse sua conta com a senh:a <h2><strong>{newPassword}</strong></h2>. </p>
                            <p>Em seguida, vá até o menu 'Usuário > Meus Dados' e modifique a senha.
                            ";
                        emailModel.Subject = "Recuperação de Senha - Sistema de Controle de Contas";

                        //enviando a senha para o email do usuário
                        EmailMessageService.SendMessage(emailModel);

                        //alterar senha no banco
                        userRepo.UpdatePassword(user.IdUser, newPassword);

                        TempData["Message"] = "Senha Alterada com sucesso. Verifique sua caixa de e-mail";
                        ModelState.Clear();

                    }
                    else
                    {
                        TempData["MessageAlert"] = "Usuário não encontrado";
                    }
                }
                catch(Exception ex)
                {
                    TempData["MessageError"] = "Falha ao Recuperar Senha!";

                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            //apagar o cookie de autenticação (a identificação do usuário)
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //deslogando o usuário.


            return RedirectToAction("Login", "Account");

        }
    }
}
