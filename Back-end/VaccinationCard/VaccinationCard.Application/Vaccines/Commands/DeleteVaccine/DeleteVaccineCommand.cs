using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Domain.Shared;

namespace VaccinationCard.Application.Vaccines.Commands.DeleteVaccine
{
    public sealed record DeleteVaccineCommand(Guid VaccineId) : IRequest<Result>;
}
