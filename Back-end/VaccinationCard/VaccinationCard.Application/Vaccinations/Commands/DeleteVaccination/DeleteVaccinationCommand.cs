using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccinations.Commands.DeleteVaccination
{
    public sealed record DeleteVaccinationCommand(Guid VaccinationId) : IRequest<Result>;
}
