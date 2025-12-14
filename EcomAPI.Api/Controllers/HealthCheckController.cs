using EcomAPI.Api.Controllers.Base;
using EcomAPI.Service.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EcomAPI.Api.Controllers
{
    [Route("[controller]")]
    public class HealthCheckController : BaseController
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly IHealthCheckService _healthCheckService;

        public HealthCheckController(ILogger<HealthCheckController> logger, IHealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) =>
            Ok(await _healthCheckService.CheckServiceHealthAsync());



        [HttpPost]
        public async Task<IActionResult> Post(int id) =>
            Ok(await _healthCheckService.CheckServiceHealthAsync());
    }
}