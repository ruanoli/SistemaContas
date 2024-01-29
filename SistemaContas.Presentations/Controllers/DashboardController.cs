using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaContas.Data.Repositories;

namespace SistemaContas.Presentations.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var bill = new BillRepository();

            return View();
        }
    }
}
