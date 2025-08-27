using Domain.Abstractions;
using FluentValidation;
using MediatR;
using System.Net;

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
                var error = new Error("Validation.Error", "Erro na validação", failures!);

                return HandleErrorValidationResult(error);
            }
                    
            
            return await next(cancellationToken);
        }

        // Adicionado tratamento no ValidationBehavior para retornar falhas de validação
        // no formato exato esperado pelo handler, evitando erros. Agora, o HandleErrorValidationResult identifica dinamicamente
        // se o tipo de retorno é Result ou Result<T> e invoca o método Failure apropriado.
        private TResponse HandleErrorValidationResult(Error error)
        {
            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var genericType = typeof(TResponse).GetGenericArguments()[0];
                var failureMethod = typeof(Result<>)
                    .MakeGenericType(genericType)
                    .GetMethod(nameof(Result<object>.Failure), [typeof(Error), typeof(HttpStatusCode)])!;

                var failureInstance = failureMethod.Invoke(null, new object[] { error, HttpStatusCode.BadRequest })!;
                return (TResponse)failureInstance;
            }

            return (TResponse)(object)Result.Failure(error, HttpStatusCode.BadRequest);
        }
    }
}