using System.Diagnostics;
using CheckBoxesAspCore6.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheckBoxesAspCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Single Checkbox.............

        //public IActionResult Index()
        //{
        //    var model = new ViewModell()
        //    {
        //        AcceptTerms = false,
        //        Text = "I accept the terms."
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Index(ViewModell data)
        //{
        //    var value = data.AcceptTerms;
        //    return View(data);
        //}

        //Multiple Checkboxes.........
        public IActionResult Index()
        {
            var model = new ViewModell
            {
                CheckBoxes = new List<CheckBoxOptions>
                {
                    new CheckBoxOptions
                    {
                        IsChecked = true,
                        Text = "Cricket",
                        Value = "Cricket",
                    },

                    new CheckBoxOptions
                    {
                        IsChecked = false,
                        Text = "Football",
                        Value = "Football",
                    },

                    new CheckBoxOptions
                    {
                        IsChecked = false,
                        Text = "Hockey",
                        Value = "Hockey",
                    }
                }

            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(ViewModell model)
        {
            return View(model);
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
