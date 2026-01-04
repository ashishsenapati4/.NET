using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Transaction/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            
            if(id == 0)
            {
                PopulateCategory();
                return View(new Transaction());
            }
            else
            {
                PopulateCategory();
                return View(_context.Transactions.Find(id));
            }
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if(transaction.CategoryId == 0)
            {
                ModelState.AddModelError(nameof(transaction.CategoryId), "Please Select a category");
                PopulateCategory();
                return View(transaction);
            }
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                {
                    _context.Add(transaction);
                    TempData["msg"] = "Added successfully";
                }
                else
                {
                    _context.Transactions.Update(transaction);
                    TempData["msg"] = "Updated successfully";
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategory();
            return View(transaction);
            
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategory()
        {
            var categories = _context.Categories.ToList();
            var defCategory = new Category { CategoryId = 0, Title = "Choose a Category" };
            categories.Insert(0,defCategory);
            ViewBag.Categories = categories;
        }

    }
}
