using Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinationCard.Application.Persons.Commands.UpdatePerson
{
    public sealed record UpdatePersonCommand(Guid PersonId, string Name, string CPF, string Email, string PhoneNumber, string Gender, int Age) : IRequest<Result>;
}
