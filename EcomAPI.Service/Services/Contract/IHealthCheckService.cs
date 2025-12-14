using System;
using System.Threading.Tasks;

namespace EcomAPI.Service.Services.Contract
{
    public interface IHealthCheckService
    {
        public Task<DateTime> CheckServiceHealthAsync();
    }
}