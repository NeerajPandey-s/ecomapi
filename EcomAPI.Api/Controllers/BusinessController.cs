using EcomAPI.Api.Controllers.Base;
using EcomAPI.Service.Model.Business.Request;
using EcomAPI.Service.Model.Business.Response;
using EcomAPI.Service.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using static EcomAPI.Api.Startup.ActionFilters.ApiResponseFilter;
using static EcomAPI.Service.Model.ValidationConversions;

namespace EcomAPI.Api.Controllers
{
    [Route("[controller]")]
    public class BusinessController(IBusinessService _businessService) : BaseController
    {
        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(ApiResponse<CreateBusinessResponse>))]
        //todo: [CustomAuthorizationFilter([UserRoleLookup.SuperAdmin])]
        public async Task<IActionResult> Create(CreateBusinessRequest request, CancellationToken token)
        {
            var response = await _businessService.CreateBusiness(request, token);
            return response.Match<IActionResult>(business => CreatedAtRoute("GetBusinessById", new { id = business.Id }, business),
                failed => BadRequest(failed.MapToResponse()));
        }

        [HttpGet("Get", Name = "GetBusinessById")]
        [ProducesDefaultResponseType(typeof(ApiResponse<CreateBusinessResponse>))]
        //todo: [CustomAuthorizationFilter([UserRoleLookup.SuperAdmin])]
        public async Task<IActionResult> GetBusinessById(int businessId, CancellationToken token)
        {
            var response = await _businessService.GetBusinessById(businessId, token);
            if(response is not null)
            {
                return Ok(response);
            }

            return NotFound();
        }
    }
}
