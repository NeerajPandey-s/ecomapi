using EcomAPI.Service.Data;
using EcomAPI.Service.Model;
using EcomAPI.Service.Model.Business.Request;
using EcomAPI.Service.Model.Business.Response;
using System.Threading;
using System.Threading.Tasks;

namespace EcomAPI.Service.Services.Contract
{
    public interface IBusinessService
    {
        Task<ServiceResult<CreateBusinessResponse, ValidationFailed>> CreateBusiness(CreateBusinessRequest createBusinessRequest, CancellationToken cancellationToken);
        Task<GetBusinessByIdResponse?> GetBusinessById(int businessId, CancellationToken cancellationToken);
    }
}
