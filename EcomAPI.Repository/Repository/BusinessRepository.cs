using EcomAPI.Common.ServiceInstallers.Attributes;
using EcomAPI.Repository.Repository.Contract;
using EcomAPI.Repository.Setup;
using EcomAPI.Repository.Setup.Dto;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcomAPI.Repository.Repository
{
    [ScopedService]
    internal class BusinessRepository(AppDbContext _db) : IBusinessRepository
    {
        public async Task<BusinessDto> CreateBusiness(BusinessDto business, CancellationToken token = default)
        {
            _db.Business.Add(business);
            await _db.SaveChangesAsync(token);
            return business;
        }

        public async Task<BusinessDto> UpdateBusiness(BusinessDto business, CancellationToken token = default)
        {
            _db.Business.Update(business);
            await _db.SaveChangesAsync(token);
            return business;
        }

        public async Task<BusinessDto?> GetBusinessById(int businessId, CancellationToken token = default)
        {
            return await _db.Business.FirstOrDefaultAsync(x => x.Id == businessId, token);
        }

        public async Task<BusinessDto?> GetBusinessByDomainName(string domainName, CancellationToken token = default)
        {
            return await _db.Business.FirstOrDefaultAsync(x => x.DomainName == domainName, token);
        }

        public async Task<List<BusinessDto>?> ListBusinesses(ListBusinessFilter filter, CancellationToken token = default)
        {
            return await _db.Business
                .Where(x => (x.Name == filter.BusinessName ||
                    string.IsNullOrWhiteSpace(filter.BusinessName)))
                .ToListAsync(token);
        }
    }

    public class ListBusinessFilter
    {
        public string? BusinessName { get; set; }
    }
}
