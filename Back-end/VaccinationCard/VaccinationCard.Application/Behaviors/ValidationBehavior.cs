using FluentValidation;
using MediatR;

namespace VaccinationCard.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = (await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                            .SelectMany(result => result.Errors)
                            .Where(f => f != null)
                            .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
                    
            
            return await next(cancellationToken);
        }
    }
}