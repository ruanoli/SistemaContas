using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SistemaContas.Data.Entities;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class BillController : Controller
    {
        /// <summary>
        /// Além de abrir a página de cadastro de contas, é carregada as opções da combo CategoryList
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            var billModel = new BillRegisterModel();

            //inicializando a lista (separando um espaço na memória para armazenar as opções)
            billModel.CategoryItems = GetCategoryList();

           

            return View(billModel);
        }

        [HttpPost]
        public IActionResult Register(BillRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var userLogado = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);
                    var bill = new Bill();

                    bill.IdBill = Guid.NewGuid();
                    bill.ValueBill = model.ValueBill;
                    bill.DataBill = model.DateBill.Value;
                    bill.IdUser = userLogado.IdUser;
                    bill.Comments = model.Comments;
                    bill.IdCategory = model.IdCategory;
                    bill.TypeBill = model.Type;
                    bill.Name = model.Name;

                    var billRepository = new BillRepository();

                    billRepository.Insert(bill);


                    TempData["MessageSuccess"] = "Conta cadastrada com sucesso";

                    ModelState.Clear();
                    return RedirectToAction("Index", "Dashboard");

                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = $"Erro ao cadastrar conta. {ex.Message}";
                }
            }
            else
            {
                TempData["MessageAlert"] = "Algo deu errado! Certifique-se que o preenchimento do formulário está correto";
                
            }

            model.CategoryItems = GetCategoryList();

            return View(model);
        }

        /// <summary>
        /// Consultar contas cadastradas
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// Editar contas cadastradas
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// Foio necessário colocar a lógica de carregamento da lista separadamente, porque ele será usado no carregamento
        /// da página quando o formulário for aberto e também quando o submit for dado, ele irá retornar pra page. Para n
        /// dá NullReference é necessário carregá-lo.
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetCategoryList()
        {
            var categoyItems = new List<SelectListItem>();

            try
            {
                var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);
                var category = new CategoryRepository();
                var categoryList = category.GetAllByUser(user.IdUser);

                //indo ao banco percorrer as categorias já cadastradas e armazer o id e o nome na
                //propriedade CategoryItems que é uma List<SelectListItem>, e por isso precisa de Value
                // que é o id e Text que é o nome da categoria.
                foreach (var item in categoryList)
                {
                    var selectListItems = new SelectListItem();
                    selectListItems.Value = item.IdCategory.ToString();
                    selectListItems.Text = item.Name;


                    categoyItems.Add(selectListItems);
                }


            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Não foi possível obter as categorias" + ex.Message;
            }

            return categoyItems;

        }
    }
}
