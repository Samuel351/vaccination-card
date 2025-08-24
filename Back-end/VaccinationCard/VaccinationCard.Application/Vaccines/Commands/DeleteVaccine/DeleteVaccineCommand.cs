using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Vaccines.Commands.DeleteVaccine
{
    public sealed record DeleteVaccineCommand(Guid VaccineId) : IRequest<Result>;
}
