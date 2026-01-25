using Microsoft.AspNetCore.Mvc;
using PartialViewPractice.Models;
using System.Net;
using System.Text.Json;

namespace PartialViewPractice.Controllers
{
    public class Product2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string Category)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                WriteIndented = true
            };

            try
            {
                List<Product2> products = new List<Product2>()
                {
                    new Product2{ Id = 1001, Name = "Laptop",  Description = "Dell Laptop" },
                    new Product2{ Id = 1002, Name = "Desktop", Description = "HP Desktop" },
                    new Product2{ Id = 1003, Name = "Mobile", Description = "Apple IPhone" }
                };

                return Json(products, options);
            }
            catch(Exception e)
            {
                var errorObject = new { 
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    ExceptionType = "Internal Server Error"
                };

                return new JsonResult(errorObject, options)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
