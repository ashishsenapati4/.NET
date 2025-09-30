using System.Diagnostics;
using CodeFirstAspCore6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstAspCore6.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly StudentDbContext studentDb;

        public HomeController(StudentDbContext studentDb)
        {
            this.studentDb = studentDb;
        }

        public async Task<IActionResult> Index()
        {
            var studentData = await studentDb.Students.ToListAsync();

            return View(studentData);
        }

        public IActionResult Create()
        {
            List<SelectListItem> list = new()
            {
                new SelectListItem{Value="Male",Text="Male" },
                new SelectListItem{Value="Female", Text="Female"},
                new SelectListItem{Value="Other", Text="Other"}
            };

            ViewBag.List = list;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDb.Students.AddAsync(std);
                await studentDb.SaveChangesAsync();
                TempData["insert_success"] = "Inserted..";
                return RedirectToAction("Index","Home");
            }
            return View(std);
        }

        public async Task<IActionResult> Details(int id)
        {
            if(studentDb.Students == null)
            {
                return NotFound();
            }

            var studentData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentData == null)
            {
                return NotFound();
            }
            return View(studentData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            List<SelectListItem> list = new()
            {
                new SelectListItem{Value="Male",Text="Male" },
                new SelectListItem{Value="Female", Text="Female"},
                new SelectListItem{Value="Other", Text="Other"}
            };

            ViewBag.Gender = list;

            var studentData = await studentDb.Students.FindAsync(id);
            return View(studentData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDb.Update(std);
                await studentDb.SaveChangesAsync();
                TempData["update_success"] = "Updated..";
                return RedirectToAction("Index", "Home");
            }
            return View(std);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var stdData = await studentDb.Students.FindAsync(id);

            if(stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }

            var stdData = await studentDb.Students.FindAsync(id);

            if(stdData == null)
            {
                return NotFound();
            }

            studentDb.Remove(stdData);
            await studentDb.SaveChangesAsync();
            TempData["delete_success"] = "Deleted..";
            return RedirectToAction("Index", "Home");
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
