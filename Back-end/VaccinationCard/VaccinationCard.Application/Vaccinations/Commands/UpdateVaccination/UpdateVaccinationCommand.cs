using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination
{
    public sealed record UpdateVaccinationCommand(Guid VaccinationId,int DoseNumber, DateTime? ApplicationDate = null) : IRequest<Result>;
}