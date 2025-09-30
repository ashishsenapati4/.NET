using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionAspCore6.Models;

namespace SessionAspCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("MyKey", "SDE ");
            HttpContext.Session.SetString("MyValue", "Ashish Boss");

            //Getting the unique session ID
            TempData["SessionId"] = HttpContext.Session.Id;

            return View();
        }

        public IActionResult About()
        {
            var res = HttpContext.Session.GetString("MyKey");

            var res2 = HttpContext.Session.GetString("MyValue");
            if (res != null)
            {
                ViewBag.Data = res;
            }
            ViewBag.newData = res2;
            return View();
        }

        public IActionResult Details()
        {
            var res1 = HttpContext.Session.GetString("MyKey");
            var res2 = HttpContext.Session.GetString("MyValue");

            if (res1 != null && res2 != null)
            {
                ViewBag.Data = "Key:- " + res1 + "Value:-" + res2;
            }
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("MyKey");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
