using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Sdk.Core.Exceptions.ValidationException;

namespace Sdk.Api.Mediatr.PipelinesBehaviours
{
    public class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validator = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validator
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}
