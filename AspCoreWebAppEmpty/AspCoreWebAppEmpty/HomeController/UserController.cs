using Microsoft.AspNetCore.Mvc;

namespace RoutingWithoutMVC.HomeController
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
