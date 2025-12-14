using FluentValidation;

namespace EcomAPI.Service.Model.Business.Request
{
    public class CreateBusinessRequest
    {
        public required string Name { get; set; }
        public required string DomainName { get; set; }
        public required string Email { get; set; }
        //TODO: Password and encryption
    }


    public class CreateBusinessRequestValidator : AbstractValidator<CreateBusinessRequest>
    {
        public CreateBusinessRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DomainName).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}