using Microsoft.AspNetCore.Mvc;

namespace KampusTekWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
