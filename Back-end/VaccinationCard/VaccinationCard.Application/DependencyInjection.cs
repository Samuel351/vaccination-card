using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VaccinationCard.Application.Behaviors;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Domain.Services;

namespace VaccinationCard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePersonCommandHandler>());
            services.AddValidatorsFromAssembly(typeof(CreatePersonCommandHandler).Assembly, includeInternalTypes: true);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<VaccinationService>();

            return services;
        }
    }
}
