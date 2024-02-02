using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaContas.Data.Repositories;
using SistemaContas.Presentations.Models;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var bill = new BillQueryModel();

            try
            {
                var userLogado = JsonConvert.DeserializeObject<UserModel>(User.Identity.Name);
                var billRepo = new BillRepository();

                var currentDate = DateTime.Now;

                //Carrega a página com a data inicial, com o ano atual, mês e dia 1.
                bill.StartDate = new DateTime(currentDate.Year, currentDate.Month, 1);

                //a data fim será o último dia do mês atual.
                bill.EndDate = bill.StartDate.Value.AddMonths(1).AddDays(-1);

                bill.Bills = billRepo.GetBillAll(bill.StartDate.Value, bill.EndDate.Value, userLogado.IdUser);

                bill.TotalValueToReceive = billRepo.GetSumBillAllToReceive(userLogado.IdUser);
                bill.TotalValuePay = billRepo.GetSumBillAllPay(userLogado.IdUser);

                

                var billRepository = new BillRepository();
                bill.Bills = billRepository.GetBillAll(bill.StartDate.Value.Date, bill.EndDate.Value.Date, userLogado.IdUser);

            }
            catch (Exception ex)
            {
                TempData["MessageError"] = $"Erro ao carregar contas. {ex.Message}";

            }

            return View(bill);
        }
    }
}
