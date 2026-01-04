using BudgetBuddy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashBoardController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<ActionResult> Index()
        {

            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            //LAst 15 days transaction...
            List<Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date.Date <= EndDate.Date && y.Date.Date >= StartDate.Date)
                .ToListAsync();

            //Total income
            int totalIncome = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = totalIncome.ToString("c0");

            //Total Expense
            int totalExpense = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = totalExpense.ToString("c0");

            //Balance
            int balance = totalIncome - totalExpense;
            ViewBag.Balance = balance.ToString("c0");

            //Doughnut chart - Expense by category
            ViewBag.DoughnutChartData = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0")
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            //Spline Chart - Day vs Income & Expense

            //Income
            List<SplineChartData> IncomeSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData
                {
                    Day = k.First().Date.ToString("dd-MMM"),
                    Income = k.Sum( j => j.Amount)


                }).ToList();

            //Expense
            List<SplineChartData> ExpenseSummary = SelectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData
                {
                    Day = k.First().Date.ToString("dd-MMM"),
                    Expense = k.Sum(l => l.Amount)
                }).ToList();

            //Combine Income and Expense
            string[] Last7Days = Enumerable.Range(0, 7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.Day into DayIncomeJoined
                                      from income in DayIncomeJoined.DefaultIfEmpty() //for left-join
                                      join expense in ExpenseSummary on day equals expense.Day into DayExpenseJoined
                                      from expense in DayExpenseJoined.DefaultIfEmpty() //for left-join
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.Income,
                                          expense = expense == null ? 0 : expense.Expense
                                      };

            //select last 5 transactions..
            ViewBag.RecentTransactions = SelectedTransactions
                .OrderByDescending(t => t.TransactionId)
                .Take(5)
                .ToList();

            return View();
        }
    }

    public class SplineChartData
    {
        public string Day;
        public int Income;
        public int Expense;
    }

}
