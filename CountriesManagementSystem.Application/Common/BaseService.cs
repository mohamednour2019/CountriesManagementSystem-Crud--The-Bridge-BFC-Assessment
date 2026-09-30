using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CountriesManagementSystem.Domain.Shared;

namespace CountriesManagementSystem.Application.Common
{
    internal class BaseService
    {
        private readonly IServiceProvider _services;
        public BaseService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }
        protected async Task<DomainResult> ValidateBeforeProcess<TReqeuestDto>(TReqeuestDto request)
        {
            var validator = _services.GetService<IValidator<TReqeuestDto>>();
            if (validator is null) return DomainResult.Success();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return DomainResult.Failure(validationErrors);
            }
            return DomainResult.Success();
        }

    }
}
