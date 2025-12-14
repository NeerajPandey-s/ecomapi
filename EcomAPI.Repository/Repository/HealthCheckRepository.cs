using EcomAPI.Common.ServiceInstallers.Attributes;
using EcomAPI.Repository.Repository.Contract;
using EcomAPI.Repository.Setup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EcomAPI.Repository.Repository
{
    [ScopedService]
    public class HealthCheckRepository(AppDbContext _db) : IHealthCheckRepository
    {
        public DateTime CheckHealthAsync()
        {
            var businesses = _db.Business.Count();
            return DateTime.Now;
        }
    }
}
