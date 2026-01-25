using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TagHelpersDemo.Models;

namespace TagHelpersDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        List<Student> students;

        public HomeController(ILogger<HomeController> logger)
        {
            students = new List<Student>()
            {
               new Student() { StudentId = 101, Name = "James", Branch = "CSE", Section = "A", Gender = "Male" },
               new Student() { StudentId = 102, Name = "Smith", Branch = "ETC", Section = "B", Gender = "Male" },
               new Student() { StudentId = 103, Name = "David", Branch = "CSE", Section = "A", Gender = "Male" },
               new Student() { StudentId = 104, Name = "Sara", Branch = "CSE", Section = "A", Gender = "Female" },
               new Student() { StudentId = 105, Name = "Pam", Branch = "ETC", Section = "B", Gender = "Female" }
            };

        }



        public IActionResult Index()
        {
            
            return View(students);
        }

        public IActionResult Details(int id=101)
        {
            Student std = students.Find(x => x.StudentId == id);
            if(std != null)
            {
                return View(std);
            }
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
