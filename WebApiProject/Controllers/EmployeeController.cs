using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApiProject.Controllers
{
    [ApiController]
    [Route("[employees]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
    }
}
