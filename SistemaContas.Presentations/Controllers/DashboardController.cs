using Microsoft.AspNetCore.Mvc;

namespace SistemaContas.Presentations.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
