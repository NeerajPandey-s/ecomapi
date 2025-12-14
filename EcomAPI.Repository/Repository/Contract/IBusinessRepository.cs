
using EcomAPI.Repository.Setup.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EcomAPI.Repository.Repository.Contract
{
    public interface IBusinessRepository
    {
        Task<BusinessDto> CreateBusiness(BusinessDto user, CancellationToken token = default);
        Task<BusinessDto> UpdateBusiness(BusinessDto business, CancellationToken token = default);
        Task<BusinessDto?> GetBusinessById(int businessId, CancellationToken token = default);
        Task<BusinessDto?> GetBusinessByDomainName(string domainName, CancellationToken token = default);
        Task<List<BusinessDto>?> ListBusinesses(ListBusinessFilter filter, CancellationToken token = default);

    }
}
