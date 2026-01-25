using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterEx
{
    public class MySampleResultFilterAttribute : Attribute, IResultFilter
    {
        private readonly ILogger<MySampleResultFilterAttribute> _logger;
        private readonly Guid _myGuid;
        private readonly string _name;

        public MySampleResultFilterAttribute(ILogger<MySampleResultFilterAttribute> logger, string name="Global")
        {
            _logger = logger;
            _myGuid = Guid.NewGuid();
            _name = name;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation($"Result Filter - Before {_name} {_myGuid}");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation($"Result Filter - After {_name} {_myGuid}");
        }
    }
}
