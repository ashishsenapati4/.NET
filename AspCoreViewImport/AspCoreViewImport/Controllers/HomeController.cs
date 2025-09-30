using AspCoreViewImport.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreViewImport.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>
            {
                new Student{Id = 101, Name="Kumar", Gender="Male"},
                new Student{Id = 102, Name="Kumaran", Gender="Male"},
                new Student{Id = 103, Name="Kumari", Gender="FeMale"}
            };

            return View(students);
        }

        public IActionResult About()
        {
            List<Student> students = new List<Student>
            {
                new Student{Id = 101, Name="Kumar", Gender="Male"},
                new Student{Id = 102, Name="Kumaran", Gender="Male"},
                new Student{Id = 103, Name="Kumari", Gender="FeMale"}
            };

            return View(students);
        }

        public IActionResult Contact()
        {
            List<Student> students = new List<Student>
            {
                new Student{Id = 101, Name="Kumar", Gender="Male"},
                new Student{Id = 102, Name="Kumaran", Gender="Male"},
                new Student{Id = 103, Name="Kumari", Gender="FeMale"}
            };

            return View(students);
        }
    }
}
