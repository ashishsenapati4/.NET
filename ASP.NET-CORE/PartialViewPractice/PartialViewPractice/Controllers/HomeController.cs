using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PartialViewPractice.Models;

namespace PartialViewPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            return View();
        }

        
        public PartialViewResult Details(int ProductId)
        {
            string method = HttpContext.Request.Method;

            string? requestedWith = HttpContext.Request.Headers.XRequestedWith;

            if(method == "POST" || method == "GET")
            {
                if(requestedWith == "XMLHttpRequest") // Allow requests only via AJAX. normal GET,POST req. should be rejected...
                {
                    Product product = new Product()
                    {
                        ProductId = ProductId,
                        Name = "Test Product"
                    };

                    return PartialView("_ProductDetailsPartialView", product);
                }
            }

            return PartialView("_InvalidRequestPartialView");
            
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
