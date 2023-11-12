using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Esse método só será assionado quando lá no formulário o botão submit for pressionado.
        /// Isso é possível por conta do [HttpPost]
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost] 
        public IActionResult Register(CategoryModel category)
        {
            //Todo método que recebe dados de um formulário é preciso verificar se esses dados são válidos
            if (ModelState.IsValid)
            {
                try
                {
                    //Dados do usuário autenticado. (Cookie de autenticação)
                    var user = User.Identity?.Name;
                    var userModel = JsonConvert.DeserializeObject<UserModel>(user);

                    //Passando os dados para a model category para salvar os dados no banco.
                    var categoryModel = new Category();
                    var categoryRepository = new CategoryRepository();

                    categoryModel.Name = category.Name;
                    categoryModel.IdCategory = Guid.NewGuid();
                    categoryModel.IdUser = userModel.IdUser;

                    categoryRepository.Insert(categoryModel);

                    TempData["MensagemSucesso"] = "Categoria cadastrada com sucesso";
                }
                catch(Exception ex)
                {
                    TempData["MensagemErro"] = "Erro ao cadastrar categoria.";
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Algo deu errado! Certifique-se que o preenchimento do formulário está correto";
            }

            return View();
        }

        public IActionResult Query()
        {
            return View();
        }
    }
}
