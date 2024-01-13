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
            if (ModelState.IsValid)
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
                MessageAlert();

            }

            //model.CategoryItems = GetCategoryList();

            return View(model);
        }

        /// <summary>
        /// Consultar contas cadastradas
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            var bill = new BillQueryModel();
            bill.StartDate = DateTime.Now;
            bill.EndDate = DateTime.Now;
            return View(bill);
        }

        [HttpPost]
        public IActionResult Query(BillQueryModel billModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var billRepository = new BillRepository();
                    var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);

                    billModel.Bills = billRepository.GetBillAll(billModel.StartDate.Value, 
                                                                billModel.EndDate.Value, 
                                                                user.IdUser);

                    return View(billModel);
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = $"Erro ao consultar contas. {ex.Message}";
                }


            }
            else
            {
                MessageAlert();
            }

            return View();
        }

        /// <summary>
        /// Editar contas cadastradas
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            var bill = new BillEditModel();
            bill.CategoryItems = GetCategoryList();

            return View(bill);
        }

        [HttpPost]
        public IActionResult Edit(BillEditModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var billRepo = new BillRepository();
                    var billModel = new Bill();

                    var isExist = billRepo.GetBillById(model.IdBill);

                    if (isExist != null)
                    {
                        billModel.ValueBill = model.ValueBill;
                        billModel.DataBill = model.DateBill;
                        billModel.TypeBill = model.Type;
                        billModel.Comments = model.Comments;
                        billModel.Name = model.Name;
                        billModel.IdCategory = model.IdCategory;

                        billRepo.Update(billModel);

                        return RedirectToAction("Query");
                    }

                }
                catch (Exception ex)
                {

                    TempData["MessageError"] = "Não foi possível editar as contas" + ex.Message;
                }
            }
            else
            {
                MessageAlert();

            }
            return View(model);
        }

        /// <summary>
        /// Foio necessário colocar a lógica de carregamento da lista separadamente, porque ela será usado no carregamento
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

        public object MessageAlert()
        {
            return TempData["MessageAlert"] = "Algo deu errado! Certifique-se que o preenchimento do formulário está correto";

        }
    }
}
