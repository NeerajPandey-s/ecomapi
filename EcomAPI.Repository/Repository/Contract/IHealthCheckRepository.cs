using System;

namespace EcomAPI.Repository.Repository.Contract
{
    public interface IHealthCheckRepository
    {
        DateTime CheckHealthAsync();
    }
}
