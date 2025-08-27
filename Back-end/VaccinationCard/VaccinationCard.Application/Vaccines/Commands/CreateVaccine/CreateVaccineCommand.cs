using MediatR;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    public sealed record CreateVaccineCommand(string Name, int RequiredDoses) : IRequest<Result>;
   
}
