using EcomAPI.Common.ServiceInstallers.Attributes;
using EcomAPI.Repository.Repository.Contract;
using EcomAPI.Service.Services.Contract;
using System;
using System.Threading.Tasks;

namespace EcomAPI.Service.Services
{
    [ScopedService]
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IHealthCheckRepository _healthCheckRepository;

        public HealthCheckService(IHealthCheckRepository healthCheckRepository)
        {
            _healthCheckRepository = healthCheckRepository;
        }

        public async Task<DateTime> CheckServiceHealthAsync() =>
            _healthCheckRepository.CheckHealthAsync();
    }
}