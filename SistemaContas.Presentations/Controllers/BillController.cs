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
                    bill.Value = model.ValueBill;
                    bill.Date = model.DateBill.Value;
                    bill.IdUser = userLogado.IdUser;
                    bill.Observation = model.Comments;
                    bill.IdCategory = model.IdCategory;
                    bill.Type = model.Type;
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
        /// Consultar contas cadastradas, carrega as contas do mês atual.
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            var bill = new BillQueryModel();

            try
            {
                var userLogado = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);


                var currentDate = DateTime.Now;

                //Carrega a página com a data inicial, com o ano atual, mês e dia 1.
                bill.StartDate = new DateTime(currentDate.Year, currentDate.Month, 1);

                //a data fim será o último dia do mês atual.
                bill.EndDate = bill.StartDate.Value.AddMonths(1).AddDays(-1);

                var billRepository = new BillRepository();
                bill.Bills = billRepository.GetBillAll(bill.StartDate.Value.Date, bill.EndDate.Value.Date, userLogado.IdUser);

            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Erro ao carregar contas. {ex.Message}";

            }

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
        public IActionResult Edit(Guid id)
        {
            var model = new BillEditModel();

            var billRepository = new BillRepository();


            try
            {
                var bill = billRepository.GetBillById(id);
                var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);

                if (bill != null && user.IdUser == bill.IdUser)
                {
                    model.DateBill = bill.Date;
                    model.ValueBill = bill.Value.Value;
                    model.IdBill = bill.IdBill;
                    model.CategoryItems = GetCategoryList();
                    model.IdCategory = bill.IdCategory;
                    model.Comments = bill.Observation;
                    model.CategoryItems = GetCategoryList();
                    model.Name = bill.Name;
                    model.Type = bill.Type;
                }
                else
                {
                    RedirectToAction("Query");
                }

            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Não foi possível carregar as contas" + ex.Message;

            }


            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BillEditModel model)
        {
            if (ModelState.IsValid)
            {
                var billRepo = new BillRepository();
                var billModel = new Bill();
                try
                {
                    var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);

                    var billRepository = billRepo.GetBillById(model.IdBill);

                    billModel.IdBill = model.IdBill;
                    billModel.Value = model.ValueBill;
                    billModel.Date = model.DateBill;
                    billModel.Type = model.Type;
                    billModel.Observation = model.Comments;
                    billModel.Name = model.Name;
                    billModel.IdCategory = model.IdCategory;

                    billRepo.Update(billModel);

                    return RedirectToAction("Query");


                }
                catch (Exception ex)
                {

                    TempData["MessageError"] = "Não foi possível editar as contas" + ex.Message;
                }
            }

            model.CategoryItems = GetCategoryList();
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                var billRepository = new BillRepository();
                var bill = billRepository.GetBillById(id);
                var user = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);

                if (bill != null && user.IdUser == bill.IdUser)
                {
                    billRepository.Delete(bill);

                    TempData["MessageSuccess"] = "Conta excluída com sucesso";

                }
                else
                {
                    TempData["MessageAlert"] = "Conta não existe";

                }
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Não foi possível editar as contas" + ex.Message;

            }

            return RedirectToAction("Query");
        }

        /// <summary>
        /// Foi necessário colocar a lógica de carregamento da lista separadamente, porque ela será usado no carregamento
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
