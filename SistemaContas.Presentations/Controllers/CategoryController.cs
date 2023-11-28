﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        /// <summary>
        /// Método utilizado somente para carregar a página de cadastro.
        /// </summary>
        /// <returns></returns>
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
        public IActionResult Register(CategoryRegisterModel category)
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

                    TempData["MessageSuccess"] = "Categoria cadastrada com sucesso";
                    ModelState.Clear();

                    return RedirectToAction("Index", "Dashboard");

                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = "Erro ao cadastrar categoria.";
                }
            }
            else
            {
                TempData["MessageAlert"] = "Algo deu errado! Certifique-se que o preenchimento do formulário está correto";
            }

            return View();
        }

        /// <summary>
        /// Método utilizado para ir ao banco e listar na página as categorias do usuário autenticado.
        /// O método vai ser assionado assim que a página de consulta for aberta.
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            var listCategories = new List<CategoryQueryModel>();

            try
            {
                var user = User.Identity?.Name;
                var userModel = JsonConvert.DeserializeObject<UserModel>(user);

                var categoryRepository = new CategoryRepository();

                var categories = categoryRepository.GetAllByUser(userModel.IdUser);

                //Estou varrendo cada categoria que buscamos no banco vinculadas ao usuário autenticado
                //e para cada uma criamos o obeto model, 
                //no objeto model estamos preenchendo o id e nome da categoria.
                //depois eu adiciono o objeto na listCategory, e envio o resultado para a página.
                foreach (var category in categories)
                {
                    var model = new CategoryQueryModel();
                    model.Name = category.Name;
                    model.IdCategory = category.IdCategory;

                    listCategories.Add(model);
                }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Erro ao cadastrar categoria: {ex}";
            }

            //Passando o objeto e enviando para a página.
            return View(listCategories);
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                var categoryRepository = new CategoryRepository();
                var getId = categoryRepository.GetById(id);
                var userModel = JsonConvert.DeserializeObject<UserModel>(User.Identity?.Name);
                //verifica no banco se existe a categoria e se o usuário logado é o mesmo usuário da categoria. 
                if (getId != null && getId.IdUser == userModel.IdUser)
                {
                    categoryRepository.Delete(getId);

                    TempData["MessageSuccess"] = "Categoria deletada com sucesso";

                }
                else
                {
                    TempData["MessageAlert"] = "Algo deu errado! É provável que a categoria informada não exista no nosso banco. Verifique!";

                }

            }
            catch (Exception e)
            {
                TempData["MessageError"] = $"Erro ao Excluir categoria: {e}";

            }


            return RedirectToAction("Query");
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                var userModel = JsonConvert.DeserializeObject<UserModel>(User.Identity?.Name);
                var categoryRepository = new CategoryRepository();
                var getById = categoryRepository.GetById(id);

                if (getById != null && userModel.IdUser == getById.IdUser)
                {
                    categoryRepository.Update(getById);
                }
                else
                {
                    TempData["MessageAlert"] = "Algo deu errado! É provável que a categoria informada não exista no nosso banco. Verifique!";

                }
            }
            catch(Exception ex)
            {
                TempData["MessageError"] = $"Erro ao Editar categoria: {ex}";
            }

            return View("Register");
        }
    }
}
