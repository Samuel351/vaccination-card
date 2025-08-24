using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Vaccinations.Commands.CreateVaccination
{
    public sealed record CreateVaccinationRecordCommand(Guid PersonId, Guid VaccineId, int DoseNumber, DateTime VaccinationDate) : IRequest<Result>;
}
