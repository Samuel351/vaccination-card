using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    public sealed record CreateVaccinationCommand(Guid PersonId, Guid VaccineId, int DoseNumber, DateTime? ApplicationDate = null) : IRequest<Result>;
}
