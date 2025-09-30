using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartialViewAspCore.Models;

namespace PartialViewAspCore.Controllers
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
            return View();
        }

        public IActionResult Product()
        {
            List<Product> products = new List<Product>()
            {
                new Product{Id=100, Name="Rado Premium", Description="Description 1", Price=30000, Image="~/Images/rado.jpg"},
                new Product{Id=101, Name="Rolex Premium", Description="Description 2",Price=800000, Image="~/Images/Rolex.png"},
                new Product{Id=100, Name="Rolls-Royce Premium", Description="Description 3", Price=70000000, Image="~/Images/royce.png"}

            };

            return View(products);
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
