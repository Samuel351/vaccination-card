using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Vaccines.Commands.CreateVaccine
{
    public sealed record CreateVaccineCommand(string Name, int RequiredDoses) : IRequest<Result<bool>>;
   
}
