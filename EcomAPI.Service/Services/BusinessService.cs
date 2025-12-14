using EcomAPI.Common.ServiceInstallers.Attributes;
using EcomAPI.Repository.Repository.Contract;
using EcomAPI.Repository.Setup.Dto;
using EcomAPI.Service.Data;
using EcomAPI.Service.Model;
using EcomAPI.Service.Model.Business;
using EcomAPI.Service.Model.Business.Request;
using EcomAPI.Service.Model.Business.Response;
using EcomAPI.Service.Services.Contract;
using System.Threading;
using System.Threading.Tasks;

namespace EcomAPI.Service.Services
{
    [ScopedService]
    internal class BusinessService(IBusinessRepository _businessRepository) : IBusinessService
    {
        public async Task<ServiceResult<CreateBusinessResponse, ValidationFailed>> CreateBusiness(CreateBusinessRequest request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
            {
                return new ValidationFailed(validationResult.Errors);
            }
            var businessDto = new BusinessDto
            {
                Name = request.Name,
                DomainName = request.DomainName,
                Email = request.Email,
                EncryptedPassword = ""
            };

            var business = await _businessRepository.CreateBusiness(businessDto, cancellationToken);
            return business.ConvertToCreateBusinessResponse();
        }


        public async Task<GetBusinessByIdResponse?> GetBusinessById(int businessId, CancellationToken cancellationToken)
        {

            var business = await _businessRepository.GetBusinessById(businessId, cancellationToken);
            return business?.ConvertToGetBusinessByIdResponse();
        }
    }
}
