using System.Diagnostics;
using CRUDAppUsingAdo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAppUsingAdo.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDataAccessLayer employeeDataAccessLayer;

        public HomeController(ILogger<HomeController> logger)
        {
            employeeDataAccessLayer = new EmployeeDataAccessLayer();
        }

        public IActionResult Index()
        {
            List<Employee> emps = new List<Employee>();
            emps = employeeDataAccessLayer.GetAllEmployee();
            return View(emps);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Employee emp)
        {
            try
            {
                employeeDataAccessLayer.AddEmployee(emp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult Details(int id)
        {
            Employee emp = employeeDataAccessLayer.GetEmployeeById(id);
            return View(emp);
        }

        public IActionResult Edit(int id)
        {
            Employee emp = employeeDataAccessLayer.GetEmployeeById(id);
            return View(emp);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Edit")]
        public IActionResult EditEmployee(Employee emp)
        {
            try
            {
                employeeDataAccessLayer.UpdateEmployee(emp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult Delete(int id)
        {
            Employee emp = employeeDataAccessLayer.GetEmployeeById(id);
            return View(emp);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Employee emp)
        {
            try
            {
                employeeDataAccessLayer.DeleteEmployee(emp);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
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
