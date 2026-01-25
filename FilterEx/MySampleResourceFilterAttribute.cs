using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterEx
{
    public class MySampleResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly string _name;
        public MySampleResourceFilterAttribute(string name)
        {
            _name = name;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"Resource Filter - After  {_name}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"Resource Filter - Before  {_name}");
            context.Result = new ContentResult()
            {
                Content = "This is a short-circuited pipeline"
            };
        }
    }
}
