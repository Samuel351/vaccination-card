using MediatR;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.VaccinationRecords.Commands
{
    public sealed record CreateVaccinationRecordCommand(Guid PersonId, Guid VaccineId, int DoseNumber, DateTime VaccinationDate) : IRequest<Result<bool>>;
}
