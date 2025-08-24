using Domain.Abstractions;
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
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = (await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken))))
                            .SelectMany(result => result.Errors)
                            .Where(f => f != null)
                            .Select(x => new Error(x.PropertyName, x.ErrorMessage))
                            .Select(x => x.Description)
                            .ToList();

            if (failures.Count != 0)
            {
                return (TResponse)(object)Result.Failure(new Error("Validation.Error", string.Join(",", failures)));
            }
                    
            
            return await next(cancellationToken);
        }
    }
}