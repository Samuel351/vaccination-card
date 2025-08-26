using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationCard.Application.Interfaces.Repositories;
using VaccinationCard.Domain.Interfaces.Repositories;
using VaccinationCard.Infrastructure.Data;
using VaccinationCard.Infrastructure.Options;
using VaccinationCard.Infrastructure.Repositories;

namespace VaccinationCard.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(config.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IVaccinationRepository, VaccinationRepository>();
            services.AddScoped<IVaccineRepository, VaccineRepository>();


            return services;
        }
    }
}
