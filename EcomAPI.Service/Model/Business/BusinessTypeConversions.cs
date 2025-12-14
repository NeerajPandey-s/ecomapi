using EcomAPI.Service.Model.Business.Response;

namespace EcomAPI.Service.Model.Business
{
    internal static class BusinessTypeConversions
    {
        internal static CreateBusinessResponse ConvertToCreateBusinessResponse(this Repository.Setup.Dto.BusinessDto businessDto)
        {
            return new()
            {
                DomainName = businessDto.DomainName,
                Email = businessDto.Email,
                Id = businessDto.Id,
                Name = businessDto.Name
            };
        }

        internal static GetBusinessByIdResponse ConvertToGetBusinessByIdResponse(this Repository.Setup.Dto.BusinessDto businessDto)
        {
            return new()
            {
                DomainName = businessDto.DomainName,
                Email = businessDto.Email,
                Id = businessDto.Id,
                Name = businessDto.Name
            };
        }
    }
}
