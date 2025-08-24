using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Vaccinations.Commands.DeleteVaccination
{
    public sealed record DeleteVaccinationCommand(Guid VaccinationId) : IRequest<Result>;
}
