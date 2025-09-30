using Microsoft.AspNetCore.Mvc;

namespace InstallBootstrap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
