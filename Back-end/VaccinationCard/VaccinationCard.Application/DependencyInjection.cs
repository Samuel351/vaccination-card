using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Application.Persons.Commands.CreatePerson;

namespace VaccinationCard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreatePersonCommandHandler>());

            return services;
        }
    }
}
