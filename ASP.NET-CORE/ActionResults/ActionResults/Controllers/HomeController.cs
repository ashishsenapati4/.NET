using ActionResults.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Diagnostics;
using System.Xml.Linq;

namespace ActionResults.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region RedireResult/RedirectToRoute
        //public IActionResult Index()
        //{
        //    TempData["note"] = "Hii from Index";
        //    return RedirectToRoute("AboutRoute", new {name = "Index"});
        //    //return View();
        //}



        //public string About()
        //{
        //    return "Hello and Welcome to Dot Net Tutorials\n" + TempData["note"];
        //}

        //public string About(string name)
        //{
        //    return "Hello and Welcome to Dot Net Tutorials\n" + TempData["note"] + name;
        //}

        public IActionResult Index()
        {
            var routeValues = new { controller = "Home", action = "About", Id = 123, name = "test" };

            var redirectResult = new RedirectToRouteResult(
                routeName: null,
                routeValues: routeValues,
                permanent: false,
                fragment: "AboutSection"
                );
            return redirectResult;
        }

        public string About(int id,string name)
        {
            return "This is about page. \n Name:- " + name + " id: " + id;
        }
        #endregion

        #region StatusCodeResult

        //public IActionResult NotFoundExp()
        //{
        //    return new StatusCodeResult(404);
        //}

        //public IActionResult CustomStatusCode()
        //{
        //    return new StatusCodeResult(403);
        //}

        public IActionResult NotFoundExp()
        {
            return StatusCode(404,"Resource not found");
        }

        public IActionResult CustomStatusCode()
        {
            return StatusCode(403,"Resource not available");
        }
        #endregion

        #region FileResult
        public FileResult Download()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\wwwroot\\PDFFiles\\FullSQL.pdf";

            //Could not find a part of the path 'C:\Projects\DOT NET\ActionResults\ActionResultswwwroot\PDFFiles\FullSQL.pdf'.'

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            var fileResult = File(fileBytes, "application/pdf");

            fileResult.FileDownloadName = "SQL.pdf";
            fileResult.LastModified = new DateTimeOffset(System.IO.File.GetLastWriteTimeUtc(filePath));
            fileResult.EntityTag = new Microsoft.Net.Http.Headers.EntityTagHeaderValue("\"fileVersion1\"");
            fileResult.EnableRangeProcessing = true;

            return fileResult;
        }
        #endregion

        #region ObjectResult

        public IActionResult GetPerson()
        {
            var person = new{FirstName= "Ashish", Address="BBSR",Company="Google"};
            var result = new ObjectResult(person)
            {
                StatusCode=201,
                ContentTypes=new MediaTypeCollection{"application/json",}
            };
            return result;
        }

        #endregion
    }
}
