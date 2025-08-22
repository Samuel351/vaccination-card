using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.Persons.DTOs;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(string Name) : IRequest<Result<bool>>;
}
