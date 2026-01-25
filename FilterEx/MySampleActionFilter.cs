using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterEx
{
    public class MySampleActionFilter : Attribute, IActionFilter, IOrderedFilter
    {
        private readonly string _name;
        public MySampleActionFilter(string name, int order=0)
        {
            _name = name;
            Order = order;
        }

        public int Order //implement IOrderedFilter to determine execution order of Filter.
        {                //lesser the Order value, higher the filter in the chain of execution..
            get; set;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Action Filter - After  {_name} {Order}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Action Filter - Before {_name} {Order}");
        }
    }
}
