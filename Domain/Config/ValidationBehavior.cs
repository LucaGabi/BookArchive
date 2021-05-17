using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace BookArchive.Application {
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> where TResponse : class {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
            this.validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

            var context = new ValidationContext(request);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0) {

                object data = null;
                var code = 400;
                var message = "Validation has errors";
                var errors = failures.GroupBy(k=>k.PropertyName)
                                     .ToDictionary(k=>k.First().PropertyName,v=>v.Select(x=>x.ErrorMessage));
                var hasError = true;
                var wasHandledError = true;

                var result = Activator.CreateInstance(typeof(TResponse),
                    data, code, message, errors, hasError, wasHandledError) as TResponse;

                return Task.FromResult(result);
            }

            return next();
        }

    }
}