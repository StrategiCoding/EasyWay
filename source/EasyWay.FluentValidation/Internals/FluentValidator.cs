using EasyWay.Internals.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal sealed class FluentValidator : IEasyWayValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidator(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public IDictionary<string, string[]> Validate<T>(T objectToValidate)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();

            if (validator is null)
            {
                return new Dictionary<string, string[]>();
            }

            var result = validator.Validate(objectToValidate);

            if (result.IsValid)
            {
                return new Dictionary<string, string[]>();
            }

            var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorCode).ToArray()
            );

            return errors;
        }
    }
}
