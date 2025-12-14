using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace EcomAPI.Service.Model
{
    public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
    {
        public ValidationFailed(ValidationFailure error) : this([error])
        {
        }
        public ValidationFailed(string errorMessage) : this([new ValidationFailure("", errorMessage)])
        {
        }
    }

    public static class ValidationConversions
    {
        public static ValidationFailureResponse MapToResponse(this IEnumerable<ValidationFailure> validationFailures)
        {
            return new ValidationFailureResponse
            {
                Errors = validationFailures.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
        }

        public static ValidationFailureResponse MapToResponse(this ValidationFailed failed)
        {
            return new ValidationFailureResponse
            {
                Errors = failed.Errors.Select(x => new ValidationResponse
                {
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage
                })
            };
        }
    }
    public class ValidationFailureResponse
    {
        public required IEnumerable<ValidationResponse> Errors { get; init; }
    }

    public class ValidationResponse
    {
        public required string PropertyName { get; init; }

        public required string Message { get; init; }
    }

}
