using Microsoft.AspNetCore.Mvc;

namespace RoutingWithoutMVC.HomeController
{
    //[Route("/Home")]
    //[Route("[controller]")] //using token here
    [Route("[controller]/{action}")] //using [controller] and [action] token combiningly
    public class HomeController : Controller
    {
        [Route("")]
        // [Route("Index")]
        //[Route("[action]")]
        [Route("~/")]
        [Route("~/Home")]
        public IActionResult Index()
        {
            return View();
        }

        //[Route("About")]
        //[Route("[action]")]
        public IActionResult About()
        {
            return View();
        }

        //[Route("Details/{id?}")]
        //[Route("[action]/{id?}")]
        [Route("{id?}")]
        public int Details(int? id) //'id' is nullable here
        {
            return id ?? 67;    // '??' is the null coalescing operator, if id is null print 67.
        }
    }
}
