using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MarjiGateway.Application.Exceptions;
using MarjiGateway.Application.Models;
using MediatR;

namespace MarjiGateway.Application.Behaviours
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                throw new ModelValidationException(failures.Select(ToErrorModel), "");
            }

            return await next();
        }

        private static ErrorModel ToErrorModel(ValidationFailure failure)
        {
            return new ErrorModel
            {
                ErrorCode = failure.ErrorCode,
                ErrorMessage = failure.ErrorMessage,
                Level = ErrorLevelModel.Error,
                ParameterName = failure.PropertyName
            };
        }
    }
}