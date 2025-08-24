using Domain.Abstractions;
using MediatR;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    public sealed record CreateVaccineCommand(string Name, int RequiredDoses) : IRequest<Result>;
   
}
