using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccinationCard.Application.DTOs.Responses;
using VaccinationCard.SharedKernel;

namespace VaccinationCard.Application.Persons.Queries.GetPersonById
{
    public sealed record GetPersonByIdQuery(Guid personId) : IRequest<Result<PersonResponse>>;
}
