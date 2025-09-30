using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModelAspCore6.Models;

namespace ViewModelAspCore6.Controllers
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
            List<Student> students = new List<Student>()
            { 
                new Student{Id = 100, Name="Ashish", Gender="Male", Standard=12},
                new Student{Id = 101, Name="Rekha", Gender="Female", Standard=10},
                new Student{Id = 100, Name="Suresh", Gender="Male", Standard=12},
            };

            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher{Id = 10, Name="Mita", Qualification="BA", Salary=32000},
                new Teacher{Id = 11, Name="Sunaina", Qualification="BSC", Salary=35000},
                new Teacher{Id = 12, Name="Mohan", Qualification="BCOM", Salary=32000}
            };

            SchoolViewModel schoolViewModel = new SchoolViewModel();
            schoolViewModel.MyStudents = students;
            schoolViewModel.MyTeachers = teachers;

            return View(schoolViewModel);
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
