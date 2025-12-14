using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EcomAPI.Service
{
    public static class ValidationTypeExtension
    {
        private static IServiceProvider? ValidationServiceProviders { get; set; }

        public static void InitValidationServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddValidatorsFromAssemblyContaining<ServiceConfig>();
            ValidationServiceProviders = services.BuildServiceProvider();
        }


        public static ValidationResult Validate<T>(this T obj)
        {
            if (ValidationServiceProviders is null)
            {
                throw new ApplicationException("Validators not initialized!");
            }

            var validator = ValidationServiceProviders.GetRequiredService<IValidator<T>>();
            return validator is null ? throw new NullReferenceException("Validator not found!") : validator.Validate(obj);
        }
    }
}
