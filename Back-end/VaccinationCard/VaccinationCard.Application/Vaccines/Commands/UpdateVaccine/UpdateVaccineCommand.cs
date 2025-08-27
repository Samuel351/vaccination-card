using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccines.Commands.UpdateVaccine
{
    public sealed record UpdateVaccineCommand(Guid VaccineId, string Name, int RequiredDoses) : IRequest<Result>;
}
