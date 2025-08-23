using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(Guid personId) : IRequest<Result<bool>>;
}
