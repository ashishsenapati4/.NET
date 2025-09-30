using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TagHelperDemo.Models;

namespace TagHelperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/AshDevPro/Home/Index")]
        public string Index(Employee emp)
        {
            return "Name: "+emp.Name+" Age: "+emp.Age+" Gender: "+emp.Gender+" Salary: "+emp.Salary+ " Designation: " + emp.Designation ;
        }
        public IActionResult Contact()
        {
            return View();
        }
        public int Edit(int id)
        {
            return id;
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
